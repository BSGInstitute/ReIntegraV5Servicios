using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Services
{
    /// Autor: Lolo Zaa
    /// Fecha: 17/03/2026
    /// Version: 1.0
    /// <summary>
    /// Implementacion de IClasificacionRespuestaService.
    /// Vive en BSI.Integra.Servicios porque necesita IHttpClientFactory
    /// que no esta disponible en BSI.Integra.Aplicacion.Planificacion.
    /// </summary>
    public class ClasificacionRespuestaService : IClasificacionRespuestaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;
        private readonly ILogger<ClasificacionRespuestaService> _logger;

        public ClasificacionRespuestaService(
            IUnitOfWork unitOfWork,
            IHttpClientFactory httpClientFactory,
            ILogger<ClasificacionRespuestaService> logger)
        {
            _unitOfWork = unitOfWork;
            _httpClient = httpClientFactory.CreateClient("PythonLlm");
            _logger     = logger;
        }

        /// <summary>
        /// Obtiene los disparadores ejecutados pendientes de clasificacion desde BD.
        /// </summary>
        public async Task<List<DisparadorPendienteClasificacionDTO>> ObtenerDisparadoresPendientesAsync()
        {
            return await _unitOfWork.GestionContactoRepository
                .ObtenerDisparadoresPendientesClasificacionAsync();
        }

        /// <summary>
        /// Procesa un disparador:
        /// 1. Llama al servicio Python con los datos del disparador.
        /// 2. Si Clasificado = true → marca la ocurrencia en BD.
        /// 3. Si Clasificado = false → no toca BD (ESPERANDO).
        /// </summary>
        public async Task ProcesarDisparadorAsync(DisparadorPendienteClasificacionDTO disparador)
        {
            _logger.LogInformation(
                "Procesando disparador {Id} — canal: {Canal}",
                disparador.IdDisparadorCongelado, disparador.Canal);

            /** Llamar al servicio Python con los datos del disparador **/
            var json     = JsonConvert.SerializeObject(disparador);
            var content  = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/automatizacion/clasificar-respuesta", content);
            response.EnsureSuccessStatusCode();

            var body      = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<ClasificacionRespuestaResponseDTO>(body)
                ?? throw new Exception(
                    $"Respuesta vacia del servicio Python para disparador {disparador.IdDisparadorCongelado}");

            if (!resultado.Clasificado)
            {
                _logger.LogDebug(
                    "Disparador {Id}: ESPERANDO — sin respuesta del docente aun",
                    disparador.IdDisparadorCongelado);
                return;
            }

            /** Resultado definitivo — marcar ocurrencia en BD **/
            if (resultado.IdOcurrenciaCongelada.HasValue)
            {
                _logger.LogInformation(
                    "Disparador {Id}: CLASIFICADO → {Ocurrencia} (nivel: {Nivel})",
                    disparador.IdDisparadorCongelado,
                    resultado.NombreOcurrencia,
                    resultado.NivelConfianza);

                await _unitOfWork.GestionContactoRepository.MarcarOcurrenciaAsync(
                    new MarcarOcurrenciaRequestDTO
                    {
                        IdGestionDocenteOcurrenciaCongelada = resultado.IdOcurrenciaCongelada.Value,
                        IdGestionContacto                   = disparador.IdGestionContacto,
                        UsuarioCreacion                     = "HANGFIRE"
                    });
            }
            else
            {
                _logger.LogInformation(
                    "Disparador {Id}: CLASIFICADO sin ocurrencia (ej: NO_RESPONDIO sin config en BD)",
                    disparador.IdDisparadorCongelado);
            }
        }
    }
}
