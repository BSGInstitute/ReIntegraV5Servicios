using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion
{
    public class GestionContactoService : IGestionContactoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGestionDocenteActividadService _gestionDocenteActividadService;
        private readonly IPersonalService _personalService;
        private readonly ILogger<GestionContactoService> _logger;

        public GestionContactoService(
            IUnitOfWork unitOfWork,
            IGestionDocenteActividadService gestionDocenteActividadService,
            IPersonalService personalService,
            ILogger<GestionContactoService> logger)
        {
            _unitOfWork = unitOfWork;
            _gestionDocenteActividadService = gestionDocenteActividadService;
            _personalService = personalService;
            _logger = logger;
        }

        /// Autor: Jose Vega
        /// Fecha: 18/12/2025
        /// Version: 1.0
        /// <summary>
        /// Orquesta la inserción de la Gestión de Contacto.
        /// </summary>
        public async Task<int> ProcesarInsercionGestionAsync(CrearGestionContactoDTO dto)
        {

            //// 1. Validar Centro de Costo
            //if (!await _unitOfWork.GestionContactoRepository.ExisteCentroCostoAsync(dto.IdCentroCosto))
            //{
            //    throw new Exception($"El Centro de Costo con Id {dto.IdCentroCosto} no existe.");
            //}

            //// 2. Validar Asesor (Personal)
            //if (!await _unitOfWork.GestionContactoRepository.ExistePersonalAsync(dto.IdPersonal_Asignado))
            //{
            //    throw new Exception($"El Asesor con Id {dto.IdPersonal_Asignado} no existe.");
            //}

            //// 3. Validar Clasificación Persona
            //if (!await _unitOfWork.GestionContactoRepository.ExisteClasificacionPersonaAsync(dto.IdClasificacionPersona))
            //{
            //    throw new Exception($"La Clasificación de Persona con Id {dto.IdClasificacionPersona} no existe.");
            //}

            //// 4. Validar Fase Gestión
            //if (!await _unitOfWork.GestionContactoRepository.ExisteFaseGestionAsync(dto.IdFaseGestionContacto))
            //{
            //    throw new Exception($"La Fase de Gestión con Id {dto.IdFaseGestionContacto} no existe.");
            //}

            //// 5. Validar Origen
            //if (!await _unitOfWork.GestionContactoRepository.ExisteOrigenAsync(dto.IdOrigen))
            //{
            //    throw new Exception($"El Origen con Id {dto.IdOrigen} no existe.");
            //}

            //// Solo llegamos aquí si los IDs existen, así que es seguro consultar duplicidad(Centro Costo)
            //bool existeDuplicado = await _unitOfWork.GestionContactoRepository
            //                            .ExisteGestionActivaAsync(dto.IdClasificacionPersona, dto.IdCentroCosto);

            //if (existeDuplicado)
            //{
            //    throw new Exception("Regla de Negocio: El docente ya tiene una gestión activa para este Centro de Costo.");
            //}

            // Validar IdCentroCosto solo si no es NULL
            if (dto.IdCentroCosto.HasValue)
            {
                bool existeCentroCosto = await _unitOfWork.GestionContactoRepository.ExisteCentroCostoAsync(dto.IdCentroCosto.Value);
                if (!existeCentroCosto) throw new Exception($"El Centro de Costo {dto.IdCentroCosto} no existe.");
            }

            // Lanzamos las 4 tareas restantes al mismo tiempo a la BD
            var t2 = _unitOfWork.GestionContactoRepository.ExistePersonalAsync(dto.IdPersonal_Asignado);
            var t3 = _unitOfWork.GestionContactoRepository.ExisteClasificacionPersonaAsync(dto.IdClasificacionPersona);
            var t4 = _unitOfWork.GestionContactoRepository.ExisteFaseGestionAsync(dto.IdFaseGestionContacto);
            var t5 = _unitOfWork.GestionContactoRepository.ExisteOrigenAsync(dto.IdOrigen);

            // Esperamos a que todas terminen
            await Task.WhenAll(t2, t3, t4, t5);

            // Verificamos resultados
            if (!await t2) throw new Exception($"El Asesor {dto.IdPersonal_Asignado} no existe.");
            if (!await t3) throw new Exception($"La Clasificación {dto.IdClasificacionPersona} no existe.");
            if (!await t4) throw new Exception($"La Fase {dto.IdFaseGestionContacto} no existe.");
            if (!await t5) throw new Exception($"El Origen {dto.IdOrigen} no existe.");

            // Luego la regla de negocio...

            try
            {
                // 1. Configuración de Valores por Defecto
                bool estadoActivo = true;
                bool whatsAppDefault = false;//por definir
                int idActividadInicialDefault = 1;
                int idEstadoActividadPendiente = 1;

                DateTime fechaActual = DateTime.Now;

                // 2. Construcción de la Entidad Cabecera (T_GestionContacto)
                var nuevaGestion = new GestionContacto
                {
                    IdCentroCosto = dto.IdCentroCosto,
                    IdPersonalAsignado = dto.IdPersonal_Asignado,       // El Asesor
                    IdClasificacionPersona = dto.IdClasificacionPersona, // El Docente
                    IdFaseGestionContacto = dto.IdFaseGestionContacto,
                    IdOrigen = dto.IdOrigen,
                    IdEstadoGestionContacto = 2,
                    UltimoComentario = dto.Comentario,
                    EstadoSeguimientoWhatsApp = whatsAppDefault,
                    Estado = estadoActivo,
                    UsuarioCreacion = dto.UsuarioCreacion,
                    UsuarioModificacion = dto.UsuarioCreacion,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual
                };

                // 3. Inserción de Cabecera (Para obtener el ID)
                var tGestionContacto = _unitOfWork.GestionContactoRepository.AddAsync(nuevaGestion);

                // Guardamos para materializar el ID de la nueva gestión
                await _unitOfWork.CommitAsync();

                // 4. Construcción del Log (T_GestionContactoLog)
                var nuevoLog = new GestionContactoLog
                {
                    IdGestionContacto = tGestionContacto.Id, // FK generada arriba
                    IdCentroCosto = dto.IdCentroCosto,
                    IdPersonalAsignado = dto.IdPersonal_Asignado,
                    IdClasificacionPersona = dto.IdClasificacionPersona,
                    IdFaseGestionContacto = dto.IdFaseGestionContacto,
                    IdFaseGestionContactoAnterior = null,
                    IdOrigen = dto.IdOrigen,
                    IdEstadoGestionContacto = 2,
                    EstadoSeguimientoWhatsApp = null,
                    IdPersonalAsignadoAnterior = null,
                    IdCentroCostoAnterior = null,
                    FechaLog = fechaActual,
                    FechaFinLog = null,
                    FechaCambioFaseContacto = null,
                    CambioFaseContacto = null,
                    FechaCambioAsesor = null,
                    FechaCambioAsesorAnterior = null,
                    Comentario = "Creación Inicial: " + dto.Comentario,
                    Estado = estadoActivo,
                    UsuarioCreacion = dto.UsuarioCreacion,
                    UsuarioModificacion = dto.UsuarioCreacion,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual
                };

                // 5. Construcción de Actividad Inicial (T_ActividadDetalleGestionContacto)
                var nuevaActividad = new ActividadDetalleGestionContacto
                {
                    IdGestionContacto = tGestionContacto.Id, // FK
                    IdActividadCabecera = idActividadInicialDefault,
                    FechaProgramada = fechaActual, // Programada para hoy
                    FechaReal = null,
                    IdEstadoActividadDetalle = idEstadoActividadPendiente,
                    Comentario = "Gestión iniciada. Validar docente.",
                    Estado = estadoActivo,
                    UsuarioCreacion = dto.UsuarioCreacion,
                    UsuarioModificacion = dto.UsuarioCreacion,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual
                };

                // 6. Inserción de Detalle y Log
                _unitOfWork.GestionContactoLogRepository.AddAsync(nuevoLog);
                _unitOfWork.ActividadDetalleGestionContactoRepository.AddAsync(nuevaActividad);

                // 7. Commit Final
                await _unitOfWork.CommitAsync();

                return tGestionContacto.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Lolo Zaa
        /// Fecha: 12/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene id y nombre de centro de costos basado en un nombre parcial

        public IEnumerable<ComboDTO>ObtenerFiltroAutocomplete(string valor)
        {
          return _unitOfWork.GestionContactoRepository.ObtenerFiltroAutocomplete(valor);
        }

        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de T_PEspecifico filtrado por IdCentroCosto.
        /// </summary>
        public IEnumerable<ComboDTO> ObtenerPEspecificoPorCentroCosto(int idCentroCosto)
        {
            return _unitOfWork.GestionContactoRepository.ObtenerPEspecificoPorCentroCosto(idCentroCosto);
        }

        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones con datos del proveedor asociado a un PE especifico.
        /// </summary>
        public IEnumerable<PEspecificoSesionProveedorDTO> ObtenerSesionesProveedorPorPEspecifico(int idPEspecifico)
        {
            return _unitOfWork.GestionContactoRepository.ObtenerSesionesProveedorPorPEspecifico(idPEspecifico);
        }

        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene los flujos de gestion docente activos.
        /// </summary>
        public IEnumerable<ComboDTO> ObtenerGestionDocenteFlujos()
        {
            return _unitOfWork.GestionContactoRepository.ObtenerGestionDocenteFlujos();
        }

        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Inserta una Gestión de Contacto como oportunidad docente,
        /// resolviendo IdClasificacionPersona desde el proveedor recibido.
        /// </summary>
        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene los estados de gestion de contacto activos.
        /// </summary>
        public IEnumerable<EstadoGestionContactoDTO> ObtenerEstadosGestionContacto()
        {
            return _unitOfWork.GestionContactoRepository.ObtenerEstadosGestionContacto();
        }

        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Inserta un registro en T_GestionContactoDocenteFlujo.
        /// </summary>
        public async Task<int> InsertarGestionContactoDocenteFlujoAsync(InsertarGestionContactoDocenteFlujoDTO dto)
        {
            try
            {
                var entidad = _unitOfWork.GestionContactoRepository.InsertarGestionContactoDocenteFlujo(dto);
                await _unitOfWork.CommitAsync();
                return entidad.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> InsertarOportunidadDocenteAsync(CrearOportunidadDocenteDTO dto)
        {
            try
            {
                var clasificacion = _unitOfWork.GestionContactoRepository.ObtenerClasificacionPorProveedor(dto.IdProveedor);
                if (clasificacion == null)
                    throw new Exception($"No se encontró clasificación de persona para el proveedor {dto.IdProveedor}.");

                DateTime fechaActual = DateTime.Now;

                var nuevaGestion = new GestionContacto
                {
                    IdCentroCosto             = dto.IdCentroCosto,
                    IdPersonalAsignado        = 6205,
                    IdClasificacionPersona    = clasificacion.IdClasificacionPersona,
                    IdFaseGestionContacto     = 2,
                    IdOrigen                  = 1124,
                    IdEstadoGestionContacto   = 1,
                    UltimoComentario          = "Creacion de Oportunidad Docente Registrada",
                    EstadoSeguimientoWhatsApp = false,
                    Estado                    = true,
                    UsuarioCreacion           = dto.UsuarioCreacion,
                    UsuarioModificacion       = dto.UsuarioCreacion,
                    FechaCreacion             = fechaActual,
                    FechaModificacion         = fechaActual
                };

                var tGestionContacto = _unitOfWork.GestionContactoRepository.AddAsync(nuevaGestion);
                await _unitOfWork.CommitAsync();

                return tGestionContacto.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 23/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado paginado de oportunidades docentes.
        /// </summary>
        public OportunidadDocenteListResponseDTO ObtenerOportunidadesDocente(
            string busqueda, int pagina, int porPagina)
        {
            return _unitOfWork.GestionContactoRepository
                              .ObtenerOportunidadesDocente(busqueda, pagina, porPagina);
        }

        /// Autor: Joseph Llanque
        /// Fecha: 23/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado de docentes para el combo de tipo General.
        /// </summary>
        public IEnumerable<ComboDTO> ObtenerDocentes()
        {
            return _unitOfWork.GestionContactoRepository.ObtenerDocentes();
        }

        /// Autor: Lolo Zaa
        /// Fecha: 21/02/2026
        /// Version: 1.0
        /// <summary>
        /// Congela un flujo de gestión docente con todas sus actividades, disparadores,
        /// ocurrencias y configuración IA asociadas.
        /// </summary>
        public async Task<int> CongelarFlujoDocenteAsync(int idGestionContactoDocenteFlujo)
        {
            try
            {
                return await _unitOfWork.GestionContactoRepository.CongelarFlujoDocenteAsync(idGestionContactoDocenteFlujo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 28/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene las actividades de un flujo congelado organizadas jerarquicamente segun categoria.
        /// </summary>
        public async Task<ActividadesFlujoPorCategoriaResponseDTO> ObtenerActividadesFlujoPorCategoriaAsync(int idGestionContactoDocenteFlujo)
        {
            try
            {
                return await _unitOfWork.GestionContactoRepository.ObtenerActividadesFlujoPorCategoriaAsync(idGestionContactoDocenteFlujo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 03/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene las actividades pendientes listas para ejecutar por Hangfire.
        /// </summary>
        public async Task<List<ActividadPendienteDTO>> ObtenerActividadesPendientesAsync()
        {
            try
            {
                return await _unitOfWork.GestionContactoRepository.ObtenerActividadesPendientesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 03/03/2026
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado de una actividad despues de su ejecucion por Hangfire.
        /// </summary>
        public async Task<ResultadoEjecucionDTO> ActualizarEstadoActividadAsync(ActualizarEstadoRequestDTO request)
        {
            try
            {
                return await _unitOfWork.GestionContactoRepository.ActualizarEstadoActividadAsync(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 03/03/2026
        /// Version: 1.0
        /// <summary>
        /// Marca una ocurrencia y activa disparadores dependientes.
        /// </summary>
        public async Task<ResultadoEjecucionDTO> MarcarOcurrenciaAsync(MarcarOcurrenciaRequestDTO request)
        {
            try
            {
                return await _unitOfWork.GestionContactoRepository.MarcarOcurrenciaAsync(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 05/03/2026
        /// Version: 2.0
        /// <summary>
        /// Ejecuta una actividad especifica inmediatamente sin esperar a Hangfire.
        /// Si la actividad es automatica (Email), envia el correo realmente.
        /// </summary>
        public async Task<ResultadoEjecucionDTO> EjecutarActividadManualmenteAsync(EjecutarActividadManualDTO request)
        {
            try
            {
                // 1. Obtener datos de la actividad
                var actividad = await _unitOfWork.GestionContactoRepository.ObtenerActividadParaEjecucionManualAsync(
                    request.IdActividadDetalleCongelada,
                    request.IdDisparadorCongelado
                );

                if (actividad == null)
                {
                    return new ResultadoEjecucionDTO
                    {
                        Exitoso = false,
                        Error = "Actividad no encontrada"
                    };
                }

                // 2. Si es actividad automatica (Email), enviar correo real
                string mensajeEjecucion = null;
                if (actividad.IdTipoActividad == 1) // Automatica
                {
                    try
                    {
                        mensajeEjecucion = await EjecutarEnvioCorreoAsync(actividad);
                        _logger.LogInformation($"Ejecucion manual: {mensajeEjecucion}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error enviando correo en ejecucion manual de actividad {actividad.IdActividadDetalleCongelada}");
                        return new ResultadoEjecucionDTO
                        {
                            Exitoso = false,
                            Error = $"Error al enviar correo: {ex.Message}"
                        };
                    }
                }

                // 3. Actualizar el request con el mensaje de ejecucion si se envio correo
                if (!string.IsNullOrEmpty(mensajeEjecucion))
                {
                    // Modificar el request no es posible porque es read-only, pero podemos pasar el mensaje despues
                }

                // 4. Llamar al repository para marcar como ejecutado
                var resultado = await _unitOfWork.GestionContactoRepository.EjecutarActividadManualmenteAsync(request);

                // 5. Sobrescribir el mensaje con el del envio de correo si existe
                if (resultado.Exitoso && !string.IsNullOrEmpty(mensajeEjecucion))
                {
                    resultado.Mensaje = mensajeEjecucion;
                }

                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 05/03/2026
        /// Version: 1.0
        /// <summary>
        /// Envia correo para una actividad automatica (Email).
        /// Usa la misma logica que ActividadesCongeladasJob.
        /// </summary>
        private async Task<string> EjecutarEnvioCorreoAsync(ActividadPendienteDTO actividad)
        {
            // 1. Obtener datos de la gestion de contacto
            var gestionContacto = await _unitOfWork.GestionContactoRepository.ObtenerPorIdAsync(actividad.IdGestionContacto);
            if (gestionContacto == null || !gestionContacto.IdClasificacionPersona.HasValue)
            {
                throw new Exception($"No se encontro la gestion de contacto o no tiene clasificacion de persona");
            }

            // 2. Obtener datos del docente
            var docente = _unitOfWork.DocentePostulanteRepository.ObtenerDocenteDTOPorIdClasificacionPersona(gestionContacto.IdClasificacionPersona.Value);
            if (docente == null || string.IsNullOrWhiteSpace(docente.Correo))
            {
                throw new Exception($"No se encontro el correo del docente");
            }

            // 3. Generar la plantilla con los datos reemplazados
            var plantillaGenerada = _gestionDocenteActividadService.GenerarPlantillaDocente(new ReemplazoEtiquetaPlantillaDocenteDTO
            {
                IdGestionContacto = actividad.IdGestionContacto,
                IdPlantilla = actividad.IdPlantilla
            });

            // 4. Verificar el tipo de plantilla y ejecutar
            if (actividad.IdPlantillaBase == 2) // Email
            {
                if (string.IsNullOrWhiteSpace(plantillaGenerada.EmailReemplazado.Asunto) ||
                    string.IsNullOrWhiteSpace(plantillaGenerada.EmailReemplazado.CuerpoHTML))
                {
                    throw new Exception("La plantilla de email generada esta vacia");
                }

                // 5. Obtener credenciales de Gmail
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

                return $"[EJECUCION MANUAL] Email enviado exitosamente a {docente.Correo} - Asunto: {plantillaGenerada.EmailReemplazado.Asunto}";
            }
            else if (actividad.IdPlantillaBase == 8) // WhatsApp
            {
                // TODO: Implementar envio de WhatsApp
                return $"[PENDIENTE] WhatsApp - Plantilla: {actividad.IdPlantilla}, Contacto: {docente.Celular}";
            }
            else
            {
                throw new Exception($"Tipo de plantilla base no soportado: {actividad.IdPlantillaBase}");
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 05/03/2026
        /// Version: 1.0
        /// <summary>
        /// Adelanta la fecha de ejecucion de una actividad para que Hangfire la procese pronto.
        /// </summary>
        public async Task<ResultadoEjecucionDTO> AdelantarFechaActividadAsync(AdelantarActividadDTO request)
        {
            try
            {
                return await _unitOfWork.GestionContactoRepository.AdelantarFechaActividadAsync(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 05/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene las actividades que dependen de una actividad especifica.
        /// </summary>
        public async Task<List<ActividadDependienteDTO>> ObtenerActividadesDependientesAsync(int idActividadDetalleCongelada)
        {
            try
            {
                return await _unitOfWork.GestionContactoRepository.ObtenerActividadesDependientesAsync(idActividadDetalleCongelada);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
