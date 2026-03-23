using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Services;
using Hangfire;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Jobs
{
    /// <summary>
    /// Job de Hangfire para procesar actividades congeladas pendientes de ejecucion
    /// </summary>
    public class ActividadesCongeladasJob
    {
        private readonly IGestionContactoService _gestionContactoService;
        private readonly IActividadEnvioService _actividadEnvioService;
        private readonly ILogger<ActividadesCongeladasJob> _logger;
        private const int ID_ASESOR_SISTEMA = 6205;

        public ActividadesCongeladasJob(
            IGestionContactoService gestionContactoService,
            IActividadEnvioService actividadEnvioService,
            ILogger<ActividadesCongeladasJob> logger)
        {
            _gestionContactoService = gestionContactoService;
            _actividadEnvioService = actividadEnvioService;
            _logger = logger;
        }

        /// <summary>
        /// Procesa todas las actividades pendientes de ejecucion
        /// Este metodo es llamado por Hangfire cada 5 minutos
        /// </summary>
        [DisableConcurrentExecution(timeoutInSeconds: 300)]
        public async Task ProcesarActividadesPendientesAsync()
        {
            try
            {
                Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
                Console.WriteLine($"║  HANGFIRE - ActividadesCongeladasJob                     ║");
                Console.WriteLine($"║  INICIO: {DateTime.Now:yyyy-MM-dd HH:mm:ss}                         ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════╝");

                var actividades = await _gestionContactoService.ObtenerActividadesPendientesAsync();

                if (actividades == null || !actividades.Any())
                {
                    Console.WriteLine("  ✓ Sin actividades pendientes");
                    _logger.LogInformation("No hay actividades pendientes para procesar");
                    return;
                }

                Console.WriteLine($"  → {actividades.Count} actividad(es) pendiente(s) encontrada(s)");
                _logger.LogInformation($"Se encontraron {actividades.Count} actividades pendientes");

                int exitosas = 0;
                int fallidas = 0;

                foreach (var actividad in actividades)
                {
                    try
                    {
                        Console.WriteLine($"  ┌─ Actividad ID: {actividad.IdActividadDetalleCongelada}");
                        Console.WriteLine($"  │  Nombre:       {actividad.NombreActividad}");
                        Console.WriteLine($"  │  Tipo:         {(actividad.IdTipoActividad == 1 ? "AUTOMATICA" : "MANUAL")}");
                        Console.WriteLine($"  │  Canal:        {(actividad.IdPlantillaBase == 2 ? "EMAIL" : actividad.IdPlantillaBase == 8 ? "WHATSAPP" : $"IdPlantillaBase={actividad.IdPlantillaBase}")}");
                        Console.WriteLine($"  │  Disparador:   {actividad.IdDisparadorCongelado}");

                        _logger.LogInformation($"Procesando actividad {actividad.IdActividadDetalleCongelada} - {actividad.NombreActividad}");

                        if (actividad.IdTipoActividad == 2)
                        {
                            Console.WriteLine($"  └─ ⏭ OMITIDA (manual) — pendiente del asesor");
                            _logger.LogInformation("Actividad {Id} es Manual — omitida por Hangfire", actividad.IdActividadDetalleCongelada);
                            continue;
                        }

                        var resultado = await EjecutarActividadAsync(actividad);

                        if (resultado.Exitoso)
                        {
                            exitosas++;
                            Console.WriteLine($"  └─ ✅ EJECUTADA: {resultado.Mensaje}");
                            _logger.LogInformation($"Actividad {actividad.IdActividadDetalleCongelada} ejecutada correctamente");
                        }
                        else
                        {
                            fallidas++;
                            Console.WriteLine($"  └─ ❌ FALLO: {resultado.Error}");
                            _logger.LogWarning($"Actividad {actividad.IdActividadDetalleCongelada} fallo: {resultado.Error}");
                        }
                    }
                    catch (Exception ex)
                    {
                        fallidas++;
                        Console.WriteLine($"  └─ 💥 EXCEPCION: {ex.Message}");
                        _logger.LogError(ex, $"Error procesando actividad {actividad.IdActividadDetalleCongelada}");

                        await _gestionContactoService.ActualizarEstadoActividadAsync(new ActualizarEstadoRequestDTO
                        {
                            IdActividadDetalleCongelada = actividad.IdActividadDetalleCongelada,
                            IdDisparadorCongelado       = actividad.IdDisparadorCongelado,
                            CodigoNuevoEstado           = "NO_EJECUTADO",
                            MensajeError                = $"Excepcion no controlada: {ex.Message}",
                            UsuarioModificacion         = "HANGFIRE"
                        });
                    }
                }

                Console.WriteLine("╔══════════════════════════════════════════════════════════╗");
                Console.WriteLine($"║  FIN: {actividades.Count} procesada(s) | ✅ {exitosas} exitosa(s) | ❌ {fallidas} fallida(s)");
                Console.WriteLine($"║  {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine("╚══════════════════════════════════════════════════════════╝");

                _logger.LogInformation($"=== FIN: Procesadas {actividades.Count} actividades. Exitosas: {exitosas}, Fallidas: {fallidas} ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"💥 ERROR CRITICO en ActividadesCongeladasJob: {ex.Message}");
                _logger.LogError(ex, "Error critico en el job de actividades congeladas");
                throw;
            }
        }

        /// <summary>
        /// Ejecuta una actividad especifica segun su tipo
        /// </summary>
        private async Task<ResultadoEjecucionDTO> EjecutarActividadAsync(ActividadPendienteDTO actividad)
        {
            try
            {
                _logger.LogInformation($"Ejecutando actividad tipo {actividad.IdTipoActividad}");

                string mensajeResultado = "";

                // TODO: Implementar segun tipo de actividad
                switch (actividad.IdTipoActividad)
                {
                    case 1: // Automatica
                        mensajeResultado = await EjecutarActividadAutomaticaAsync(actividad);
                        break;

                    case 2: // Manual
                        mensajeResultado = await EjecutarActividadManualAsync(actividad);
                        break;

                    default:
                        throw new Exception($"Tipo de actividad no soportado: {actividad.IdTipoActividad}");
                }

                // Actualizar estado a EJECUTADO
                var resultado = await _gestionContactoService.ActualizarEstadoActividadAsync(new ActualizarEstadoRequestDTO
                {
                    IdActividadDetalleCongelada = actividad.IdActividadDetalleCongelada,
                    IdDisparadorCongelado = actividad.IdDisparadorCongelado,
                    CodigoNuevoEstado = "EJECUTADO",
                    MensajeResultado = mensajeResultado,
                    UsuarioModificacion = "HANGFIRE"
                });

                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  └─ 💥 NO_EJECUTADO — Actividad {actividad.IdActividadDetalleCongelada}");
                Console.WriteLine($"       Motivo: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"       Inner:  {ex.InnerException.Message}");

                _logger.LogError(ex, $"Error ejecutando actividad {actividad.IdActividadDetalleCongelada}");

                var resultado = await _gestionContactoService.ActualizarEstadoActividadAsync(new ActualizarEstadoRequestDTO
                {
                    IdActividadDetalleCongelada = actividad.IdActividadDetalleCongelada,
                    IdDisparadorCongelado       = actividad.IdDisparadorCongelado,
                    CodigoNuevoEstado           = "NO_EJECUTADO",
                    MensajeError                = ex.Message,
                    UsuarioModificacion         = "HANGFIRE"
                });

                return resultado;
            }
        }

        /// <summary>
        /// Ejecuta una actividad automatica (Email o WhatsApp) delegando al ActividadEnvioService.
        /// </summary>
        private async Task<string> EjecutarActividadAutomaticaAsync(ActividadPendienteDTO actividad)
        {
            return await _actividadEnvioService.EnviarActividadAutomaticaAsync(
                actividad, ID_ASESOR_SISTEMA, "HANGFIRE");
        }

        /// <summary>
        /// Las actividades manuales requieren accion del asesor — Hangfire no las ejecuta.
        /// Solo registra que la actividad quedo pendiente de intervencion humana.
        /// </summary>
        private Task<string> EjecutarActividadManualAsync(ActividadPendienteDTO actividad)
        {
            _logger.LogInformation(
                "Actividad {Id} es de tipo Manual — requiere accion del asesor, Hangfire no interviene",
                actividad.IdActividadDetalleCongelada);

            return Task.FromResult(
                $"Actividad manual pendiente de accion del asesor - IdGestionContacto: {actividad.IdGestionContacto}");
        }
    }
}
