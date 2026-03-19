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

            /** Construir request — telefono sin '+' segun doc v3.1 **/
            var requestBody = new
            {
                id_disparador_congelado = disparador.IdDisparadorCongelado,
                email_docente           = disparador.EmailDocente,
                telefono_docente        = disparador.TelefonoDocente?.TrimStart('+'),
                id_asesor               = disparador.IdPersonal,
                fecha_envio             = disparador.FechaEnvio.ToString("yyyy-MM-ddTHH:mm:ss"),
                mensaje_enviado         = disparador.MensajeEnviado,
                fecha_hora_inicio       = disparador.FechaHoraInicio?.ToString("yyyy-MM-ddTHH:mm:ss")
            };

            var json     = JsonConvert.SerializeObject(requestBody);
            var content  = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/automatizacion/clasificar-respuesta", content);
            response.EnsureSuccessStatusCode();

            var body      = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<ClasificacionRespuestaResponseDTO>(body)
                ?? throw new Exception(
                    $"Respuesta vacia del servicio Python para disparador {disparador.IdDisparadorCongelado}");

            // ESPERANDO — plazo no vencido, sin mensajes → no tocar BD
            if (resultado.EstadoClasificacion == "ESPERANDO")
            {
                _logger.LogDebug(
                    "Disparador {Id}: ESPERANDO — sin respuesta del docente aun",
                    disparador.IdDisparadorCongelado);
                return;
            }

            // CLASIFICADO — resultado definitivo
            var clasificacion = resultado.Clasificacion;
            if (clasificacion == null)
            {
                _logger.LogWarning(
                    "Disparador {Id}: CLASIFICADO pero sin detalle de clasificacion",
                    disparador.IdDisparadorCongelado);
                return;
            }

            _logger.LogInformation(
                "Disparador {Id}: CLASIFICADO → {Ocurrencia} | MarcadoAutomatico: {Auto} | Nivel: {Nivel}",
                disparador.IdDisparadorCongelado,
                clasificacion.NombreOcurrencia,
                clasificacion.MarcadoAutomatico,
                clasificacion.NivelConfianza);

            // MarcadoAutomatico = true → el Python YA ejecuto el SP, no hacer nada
            if (clasificacion.MarcadoAutomatico)
            {
                _logger.LogInformation(
                    "Disparador {Id}: SP ya ejecutado por el Python. Actividades dependientes en POR_EJECUTAR.",
                    disparador.IdDisparadorCongelado);
                return;
            }

            // MarcadoAutomatico = false + tiene ocurrencia → sugerencia pendiente de aprobacion del asesor
            if (!clasificacion.MarcadoAutomatico && clasificacion.IdOcurrenciaClasificada.HasValue)
            {
                _logger.LogInformation(
                    "Disparador {Id}: Sugerencia pendiente de aprobacion del asesor (ocurrencia {IdOc})",
                    disparador.IdDisparadorCongelado,
                    clasificacion.IdOcurrenciaClasificada.Value);
                return;
            }

            // MarcadoAutomatico = false + sin ocurrencia → asesor debe clasificar manualmente
            if (!clasificacion.MarcadoAutomatico && !clasificacion.IdOcurrenciaClasificada.HasValue)
            {
                _logger.LogInformation(
                    "Disparador {Id}: Sin clasificacion posible. El asesor debe clasificar manualmente.",
                    disparador.IdDisparadorCongelado);
                return;
            }
        }
    }
}
