using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using Hangfire;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Jobs
{
    /// Autor: Lolo Zaa
    /// Fecha: 17/03/2026
    /// Version: 1.0
    /// <summary>
    /// Job de Hangfire que corre cada 1 minuto.
    /// Por cada disparador ejecutado pendiente de clasificacion:
    ///   - Llama al servicio Python (localhost:8000)
    ///   - Si el Python responde CLASIFICADO → marca ocurrencia en BD
    ///   - Si responde ESPERANDO → no hace nada, vuelve al proximo tick
    /// </summary>
    public class ClasificacionRespuestaJob
    {
        private readonly IClasificacionRespuestaService _service;
        private readonly ILogger<ClasificacionRespuestaJob> _logger;

        public ClasificacionRespuestaJob(
            IClasificacionRespuestaService service,
            ILogger<ClasificacionRespuestaJob> logger)
        {
            _service = service;
            _logger  = logger;
        }

        [DisableConcurrentExecution(timeoutInSeconds: 120)]
        public async Task ProcesarClasificacionesAsync()
        {
            _logger.LogInformation("=== INICIO: Clasificacion de respuestas docentes ===");

            var disparadores = await _service.ObtenerDisparadoresPendientesAsync();

            if (disparadores == null || disparadores.Count == 0)
            {
                _logger.LogInformation("Sin disparadores pendientes de clasificacion.");
                return;
            }

            _logger.LogInformation("{N} disparadores pendientes", disparadores.Count);

            int clasificados = 0;
            int esperando    = 0;
            int fallidos     = 0;

            foreach (var disparador in disparadores)
            {
                try
                {
                    var estado = await _service.ProcesarDisparadorAsync(disparador);
                    switch (estado)
                    {
                        case "ESPERANDO": esperando++; break;
                        case "CLASIFICADO": clasificados++; break;
                        default: clasificados++; break;
                    }
                }
                catch (Exception ex)
                {
                    fallidos++;
                    _logger.LogError(ex,
                        "Error procesando disparador {Id}", disparador.IdDisparadorCongelado);
                }
            }

            _logger.LogInformation(
                "=== FIN: {Total} disparadores. Clasificados: {C}, Esperando: {E}, Fallidos: {F} ===",
                disparadores.Count, clasificados, esperando, fallidos);
        }
    }
}
