using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
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
        private readonly IGestionDocenteActividadService _gestionDocenteActividadService;
        private readonly IPersonalService _personalService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ActividadesCongeladasJob> _logger;

        public ActividadesCongeladasJob(
            IGestionContactoService gestionContactoService,
            IGestionDocenteActividadService gestionDocenteActividadService,
            IPersonalService personalService,
            IUnitOfWork unitOfWork,
            ILogger<ActividadesCongeladasJob> logger)
        {
            _gestionContactoService = gestionContactoService;
            _gestionDocenteActividadService = gestionDocenteActividadService;
            _personalService = personalService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Procesa todas las actividades pendientes de ejecucion
        /// Este metodo es llamado por Hangfire cada 5 minutos
        /// </summary>
        public async Task ProcesarActividadesPendientesAsync()
        {
            try
            {
                _logger.LogInformation("=== INICIO: Procesamiento de actividades pendientes ===");

                // 1. Obtener actividades pendientes
                var actividades = await _gestionContactoService.ObtenerActividadesPendientesAsync();

                if (actividades == null || !actividades.Any())
                {
                    _logger.LogInformation("No hay actividades pendientes para procesar");
                    return;
                }

                _logger.LogInformation($"Se encontraron {actividades.Count} actividades pendientes");

                // 2. Procesar cada actividad
                int exitosas = 0;
                int fallidas = 0;

                foreach (var actividad in actividades)
                {
                    try
                    {
                        _logger.LogInformation($"Procesando actividad {actividad.IdActividadDetalleCongelada} - {actividad.NombreActividad}");

                        var resultado = await EjecutarActividadAsync(actividad);

                        if (resultado.Exitoso)
                        {
                            exitosas++;
                            _logger.LogInformation($"Actividad {actividad.IdActividadDetalleCongelada} ejecutada correctamente");
                        }
                        else
                        {
                            fallidas++;
                            _logger.LogWarning($"Actividad {actividad.IdActividadDetalleCongelada} fallo: {resultado.Error}");
                        }
                    }
                    catch (Exception ex)
                    {
                        fallidas++;
                        _logger.LogError(ex, $"Error procesando actividad {actividad.IdActividadDetalleCongelada}");

                        // Registrar el error en la base de datos
                        await _gestionContactoService.ActualizarEstadoActividadAsync(new ActualizarEstadoRequestDTO
                        {
                            IdActividadDetalleCongelada = actividad.IdActividadDetalleCongelada,
                            IdDisparadorCongelado = actividad.IdDisparadorCongelado,
                            CodigoNuevoEstado = "NO_EJECUTADO",
                            MensajeError = $"Excepcion no controlada: {ex.Message}",
                            UsuarioModificacion = "HANGFIRE"
                        });
                    }
                }

                _logger.LogInformation($"=== FIN: Procesadas {actividades.Count} actividades. Exitosas: {exitosas}, Fallidas: {fallidas} ===");
            }
            catch (Exception ex)
            {
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
                _logger.LogError(ex, $"Error ejecutando actividad {actividad.IdActividadDetalleCongelada}");

                // Actualizar estado a NO_EJECUTADO
                var resultado = await _gestionContactoService.ActualizarEstadoActividadAsync(new ActualizarEstadoRequestDTO
                {
                    IdActividadDetalleCongelada = actividad.IdActividadDetalleCongelada,
                    IdDisparadorCongelado = actividad.IdDisparadorCongelado,
                    CodigoNuevoEstado = "NO_EJECUTADO",
                    MensajeError = ex.Message,
                    UsuarioModificacion = "HANGFIRE"
                });

                return resultado;
            }
        }

        /// <summary>
        /// Ejecuta una actividad automatica (Email, WhatsApp, SMS)
        /// </summary>
        private async Task<string> EjecutarActividadAutomaticaAsync(ActividadPendienteDTO actividad)
        {
            try
            {
                _logger.LogInformation($"Iniciando ejecucion automatica - IdGestionContacto: {actividad.IdGestionContacto}, IdPlantilla: {actividad.IdPlantilla}, IdPlantillaBase: {actividad.IdPlantillaBase}");

                // 1. Obtener datos de la gestion de contacto para obtener el docente
                var gestionContacto = await _unitOfWork.GestionContactoRepository.ObtenerPorIdAsync(actividad.IdGestionContacto);
                if (gestionContacto == null || gestionContacto.Id == 0)
                {
                    throw new Exception($"No se encontro la gestion de contacto con Id {actividad.IdGestionContacto}");
                }

                if (!gestionContacto.IdClasificacionPersona.HasValue)
                {
                    throw new Exception($"La gestion de contacto {actividad.IdGestionContacto} no tiene clasificacion de persona asociada");
                }

                // 2. Obtener datos del docente
                var docente = _unitOfWork.DocentePostulanteRepository.ObtenerDocenteDTOPorIdClasificacionPersona(gestionContacto.IdClasificacionPersona.Value);
                if (docente == null || string.IsNullOrWhiteSpace(docente.Correo))
                {
                    throw new Exception($"No se encontro el correo del docente para la clasificacion de persona {gestionContacto.IdClasificacionPersona.Value}");
                }

                // 3. Generar la plantilla con los datos reemplazados
                _logger.LogInformation($"Generando plantilla {actividad.IdPlantilla} para gestion contacto {actividad.IdGestionContacto}");
                var plantillaGenerada = _gestionDocenteActividadService.GenerarPlantillaDocente(new ReemplazoEtiquetaPlantillaDocenteDTO
                {
                    IdGestionContacto = actividad.IdGestionContacto,
                    IdPlantilla = actividad.IdPlantilla
                });

                // 4. Verificar el tipo de plantilla y ejecutar segun corresponda
                if (actividad.IdPlantillaBase == 2) // Email
                {
                    if (string.IsNullOrWhiteSpace(plantillaGenerada.EmailReemplazado.Asunto) ||
                        string.IsNullOrWhiteSpace(plantillaGenerada.EmailReemplazado.CuerpoHTML))
                    {
                        throw new Exception("La plantilla de email generada esta vacia");
                    }

                    // 5. Obtener credenciales de Gmail desde configuracion
                    var configCorreo = _unitOfWork.ConfiguracionIntegraRepository.ObtenerPorClaves("GMAIL", "CORREO_REMITENTE");
                    var configClave = _unitOfWork.ConfiguracionIntegraRepository.ObtenerPorClaves("GMAIL", "CLAVE_APLICACION");

                    if (configCorreo == null || string.IsNullOrWhiteSpace(configCorreo.Valor1))
                    {
                        throw new Exception("No se encontro la configuracion del correo remitente de Gmail");
                    }

                    if (configClave == null || string.IsNullOrWhiteSpace(configClave.Valor1))
                    {
                        throw new Exception("No se encontro la configuracion de la clave de aplicacion de Gmail");
                    }

                    // 6. Enviar el correo
                    _logger.LogInformation($"Enviando correo a {docente.Correo} con asunto: {plantillaGenerada.EmailReemplazado.Asunto}");

                    bool enviado = _personalService.EnviarCorreoGmail(
                        emailDestinatario: docente.Correo,
                        emailRemitente: configCorreo.Valor1,
                        personal: "BSG Institute - Planificacion Docente",
                        clave: configClave.Valor1,
                        mensaje: plantillaGenerada.EmailReemplazado.CuerpoHTML,
                        asunto: plantillaGenerada.EmailReemplazado.Asunto
                    );

                    if (!enviado)
                    {
                        throw new Exception("El servicio de envio de correo retorno false");
                    }

                    _logger.LogInformation($"Correo enviado exitosamente a {docente.Correo}");
                    return $"Email enviado exitosamente a {docente.Correo} - Asunto: {plantillaGenerada.EmailReemplazado.Asunto}";
                }
                else if (actividad.IdPlantillaBase == 8) // WhatsApp
                {
                    // TODO: Implementar envio de WhatsApp en siguiente fase
                    _logger.LogWarning($"Envio de WhatsApp aun no implementado - IdPlantilla: {actividad.IdPlantilla}");
                    return $"[PENDIENTE] WhatsApp - Plantilla: {actividad.IdPlantilla}, Contacto: {docente.Celular}";
                }
                else
                {
                    throw new Exception($"Tipo de plantilla base no soportado: {actividad.IdPlantillaBase}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error ejecutando actividad automatica {actividad.IdActividadDetalleCongelada}");
                throw new Exception($"Error en actividad automatica: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Ejecuta una actividad manual (Crear notificacion o tarea)
        /// </summary>
        private async Task<string> EjecutarActividadManualAsync(ActividadPendienteDTO actividad)
        {
            // TODO: Implementar creacion de notificaciones/tareas en siguiente paso
            // Por ahora solo simulamos la ejecucion

            await Task.Delay(100); // Simular procesamiento

            return $"[SIMULACION] Notificacion creada para contacto: {actividad.IdGestionContacto}";
        }
    }
}
