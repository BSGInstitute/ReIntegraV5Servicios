using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion.WhatsAppMensajeEnviadoApiPlanificacionDTO;


namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class GestionContactoController : ControllerBase
    {
        private readonly IGestionContactoService _gestionContactoService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGestionDocenteActividadService _gestionDocenteActividadService;

        public GestionContactoController(
            IGestionContactoService gestionContactoService,
            IUnitOfWork unitOfWork,
            IGestionDocenteActividadService gestionDocenteActividadService)
        {
            _gestionContactoService = gestionContactoService;
            _unitOfWork = unitOfWork;
            _gestionDocenteActividadService = gestionDocenteActividadService;
        }

        /// Autor: Jose Vega
        /// Fecha: 18/12/2025
        /// Version: 1.0
        /// <summary>
        /// Inserta una Gestión de Contacto recibiendo directamente los IDs vinculados.
        /// </summary>
        [HttpPost("Insertar")]
        public async Task<IActionResult> Insertar([FromBody] CrearGestionContactoDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Exito = false, Mensaje = "Modelo inválido", Errores = ModelState });

                var idGenerado = await _gestionContactoService.ProcesarInsercionGestionAsync(dto);

                return Ok(new { Exito = true, Mensaje = "Gestión creada correctamente", Id = idGenerado });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 12/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene id y nombre de centros de costo basado en un nombre pracial
        /// <param name=-='filtros'> Filtros que contiene el nombre Parcial </param>
        /// <returns> retorna 200 y lista de objetos para combo y mensaje de error </return>
        [HttpPost("[action]")]
        public IActionResult ObtenerAutocomplete([FromBody] StringDTO filtro)
        {
          if (!ModelState.IsValid)
          {
            return BadRequest(ModelState);
          }
          return Ok(_gestionContactoService.ObtenerFiltroAutocomplete(filtro.Valor));
        }

        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de T_PEspecifico filtrado por IdCentroCosto.
        /// </summary>
        /// <param name="idCentroCosto">Identificador del centro de costo</param>
        /// <returns>Lista de objetos con Id y Nombre</returns>
        [HttpGet("[action]/{idCentroCosto}")]
        public IActionResult ObtenerPEspecificoPorCentroCosto(int idCentroCosto)
        {
            try
            {
                return Ok(_gestionContactoService.ObtenerPEspecificoPorCentroCosto(idCentroCosto));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones con datos del proveedor asociado a un PE especifico.
        /// </summary>
        /// <param name="idPEspecifico">Identificador del presupuesto especifico</param>
        /// <returns>Lista de IdPEspecificoSesion, IdProveedor y NombreProveedor</returns>
        [HttpGet("[action]/{idPEspecifico}")]
        public IActionResult ObtenerSesionesProveedorPorPEspecifico(int idPEspecifico)
        {
            try
            {
                return Ok(_gestionContactoService.ObtenerSesionesProveedorPorPEspecifico(idPEspecifico));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene los flujos de gestion docente activos.
        /// </summary>
        /// <returns>Lista de Id y Nombre de los flujos activos</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerGestionDocenteFlujos()
        {
            try
            {
                return Ok(_gestionContactoService.ObtenerGestionDocenteFlujos());
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }
        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene los estados de gestion de contacto activos.
        /// </summary>
        /// <returns>Lista de Id, Nombre y Descripcion de los estados activos</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerEstadosGestionContacto()
        {
            try
            {
                return Ok(_gestionContactoService.ObtenerEstadosGestionContacto());
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }
        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Inserta una Gestión de Contacto como oportunidad docente a partir de
        /// IdCentroCosto e IdProveedor. El IdClasificacionPersona se resuelve
        /// automáticamente desde el proveedor indicado.
        /// </summary>
        /// Autor: Lolo Zaa
        /// Fecha: 13/02/2026
        /// Version: 1.0
        /// <summary>
        /// Inserta un registro en T_GestionContactoDocenteFlujo vinculando
        /// una Gestion de Contacto con un Flujo de Gestion Docente.
        /// </summary>
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertarGestionContactoDocenteFlujo([FromBody] InsertarGestionContactoDocenteFlujoDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Exito = false, Mensaje = "Modelo inválido", Errores = ModelState });

                var idGenerado = await _gestionContactoService.InsertarGestionContactoDocenteFlujoAsync(dto);

                return Ok(new { Exito = true, Mensaje = "Registro creado correctamente", Id = idGenerado });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito   = false,
                    Mensaje = ex.Message,
                    Detalle = ex.InnerException?.Message,
                    Inner2  = ex.InnerException?.InnerException?.Message
                });
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> InsertarOportunidadDocente([FromBody] CrearOportunidadDocenteDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Exito = false, Mensaje = "Modelo inválido", Errores = ModelState });

                var idGenerado = await _gestionContactoService.InsertarOportunidadDocenteAsync(dto);

                return Ok(new { Exito = true, Mensaje = "Oportunidad docente creada correctamente", Id = idGenerado });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito   = false,
                    Mensaje = ex.Message,
                    Detalle = ex.InnerException?.Message,
                    Inner2  = ex.InnerException?.InnerException?.Message
                });
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 21/02/2026
        /// Version: 1.0
        /// <summary>
        /// Congela un flujo de gestión docente con todas sus actividades, disparadores,
        /// ocurrencias y configuración IA asociadas. Crea copias congeladas en estado
        /// POR_EJECUTAR para ejecución posterior.
        /// </summary>
        /// <param name="idGestionContactoDocenteFlujo">ID del vínculo entre gestión contacto y flujo docente a congelar</param>
        /// <returns>ID del flujo congelado creado</returns>
        [HttpPost("[action]/{idGestionContactoDocenteFlujo}")]
        public async Task<IActionResult> CongelarFlujoDocente(int idGestionContactoDocenteFlujo)
        {
            try
            {
                var idFlujoCongelado = await _gestionContactoService.CongelarFlujoDocenteAsync(idGestionContactoDocenteFlujo);

                return Ok(new
                {
                    Exito = true,
                    Mensaje = "Flujo docente congelado correctamente",
                    IdFlujoCongelado = idFlujoCongelado
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito   = false,
                    Mensaje = ex.Message,
                    Detalle = ex.InnerException?.Message,
                    Inner2  = ex.InnerException?.InnerException?.Message
                });
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 23/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado de docentes para el combo del formulario General.
        /// </summary>
        [HttpGet("[action]")]
        public IActionResult ObtenerDocentes()
        {
            try
            {
                return Ok(_gestionContactoService.ObtenerDocentes());
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 23/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado paginado de oportunidades docentes para la grilla.
        /// </summary>
        [HttpGet("[action]")]
        public IActionResult ObtenerOportunidades(
            [FromQuery] string? busqueda = null,
            [FromQuery] int pagina = 1,
            [FromQuery] int porPagina = 10)
        {
            try
            {
                return Ok(_gestionContactoService.ObtenerOportunidadesDocente(
                    busqueda ?? "", pagina, porPagina));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 28/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene las actividades de un flujo docente congelado organizadas jerarquicamente.
        /// Para categoria General (1): retorna estructura { Actividades: [{ Detalles: [{ Disparadores: [...] }] }] }
        /// Para categoria Ejecucion Curso (2): retorna estructura { Sesiones: [{ Actividades: [{ Detalles: [{ Disparadores: [...] }] }] }] }
        /// </summary>
        /// <param name="idGestionContactoDocenteFlujo">ID del vinculo entre gestion contacto y flujo docente</param>
        /// <returns>Estructura jerarquica de actividades segun categoria del flujo</returns>
        [HttpGet("[action]/{idGestionContactoDocenteFlujo}")]
        public async Task<IActionResult> ObtenerActividadesFlujoPorCategoria(int idGestionContactoDocenteFlujo)
        {
            try
            {
                var resultado = await _gestionContactoService.ObtenerActividadesFlujoPorCategoriaAsync(idGestionContactoDocenteFlujo);
                return Ok(new
                {
                    Exito = true,
                    Mensaje = "Actividades obtenidas correctamente",
                    Datos = resultado
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Detalle = ex.InnerException?.Message,
                    Inner2 = ex.InnerException?.InnerException?.Message
                });
            }
        }

        // =============================================
        // ENDPOINTS PARA HANGFIRE
        // =============================================

        /// Autor: Lolo Zaa
        /// Fecha: 03/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de actividades pendientes listas para ejecutar por Hangfire.
        /// Incluye actividades con disparadores de TIEMPO_FIJO y TIEMPO_RELATIVO que estan
        /// dentro de la ventana de ejecucion (5 minutos).
        /// </summary>
        /// <returns>Lista de actividades pendientes con datos de plantilla y contacto</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenerActividadesPendientes()
        {
            try
            {
                var actividades = await _gestionContactoService.ObtenerActividadesPendientesAsync();
                return Ok(new
                {
                    Exito = true,
                    Mensaje = "Actividades pendientes obtenidas correctamente",
                    Cantidad = actividades.Count,
                    Datos = actividades
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Detalle = ex.InnerException?.Message
                });
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 03/03/2026
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado de una actividad congelada despues de su ejecucion por Hangfire.
        /// Registra el resultado en el historial de ejecuciones y actualiza los estados de
        /// la actividad y disparador. Maneja reintentos automaticamente (maximo 3 intentos).
        /// </summary>
        /// <param name="request">Datos de la actualizacion de estado</param>
        /// <returns>Resultado de la operacion con el ID del registro de ejecucion creado</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> ActualizarEstadoActividad([FromBody] ActualizarEstadoRequestDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Exito = false, Mensaje = "Modelo invalido", Errores = ModelState });

                var resultado = await _gestionContactoService.ActualizarEstadoActividadAsync(request);

                if (resultado.Exitoso)
                {
                    return Ok(new
                    {
                        Exito = true,
                        Mensaje = resultado.Mensaje,
                        IdEjecucion = resultado.IdRegistro
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Exito = false,
                        Mensaje = resultado.Error
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Detalle = ex.InnerException?.Message
                });
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 03/03/2026
        /// Version: 1.0
        /// <summary>
        /// Marca una ocurrencia y activa disparadores dependientes convirtiendolos a TIEMPO_FIJO.
        /// Busca todas las actividades con disparador OCURRENCIA_PREVIA que dependen de la
        /// ocurrencia marcada, calcula su fecha de ejecucion y las convierte a TIEMPO_FIJO
        /// para que Hangfire las procese.
        /// </summary>
        /// <param name="request">Datos de la ocurrencia a marcar</param>
        /// <returns>Resultado de la operacion con el ID de la ocurrencia marcada</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> MarcarOcurrencia([FromBody] MarcarOcurrenciaRequestDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Exito = false, Mensaje = "Modelo invalido", Errores = ModelState });

                var resultado = await _gestionContactoService.MarcarOcurrenciaAsync(request);

                if (resultado.Exitoso)
                {
                    return Ok(new
                    {
                        Exito = true,
                        Mensaje = resultado.Mensaje,
                        IdOcurrenciaMarcada = resultado.IdRegistro
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Exito = false,
                        Mensaje = resultado.Error
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Detalle = ex.InnerException?.Message
                });
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 05/03/2026
        /// Version: 1.0
        /// <summary>
        /// Ejecuta una actividad especifica inmediatamente sin esperar al ciclo de Hangfire.
        /// Util para pruebas o ejecuciones forzadas por el usuario.
        /// </summary>
        /// <param name="request">Datos de la actividad a ejecutar</param>
        /// <returns>Resultado de la ejecucion</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> EjecutarActividadManualmente([FromBody] EjecutarActividadManualDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Exito = false, Mensaje = "Modelo invalido", Errores = ModelState });

                var resultado = await _gestionContactoService.EjecutarActividadManualmenteAsync(request);

                if (resultado.Exitoso)
                {
                    // Si la actividad es automatica (IdTipoActividad == 1), enviar segun medio de comunicacion
                    string mensajeEnvio = null;
                    if (resultado.DatosActividad != null &&
                        resultado.DatosActividad.IdTipoActividad == 1)
                    {
                        try
                        {
                            if (resultado.DatosActividad.IdPlantillaBase == 2)
                            {
                                // Email
                                mensajeEnvio = await EnviarCorreoActividadAsync(resultado.DatosActividad);
                            }
                            else if (resultado.DatosActividad.IdPlantillaBase == 8)
                            {
                                // WhatsApp
                                mensajeEnvio = await EnviarWhatsAppActividadAsync(resultado.DatosActividad);
                            }
                        }
                        catch (Exception ex)
                        {
                            return BadRequest(new
                            {
                                Exito = false,
                                Mensaje = "Actividad ejecutada pero error al enviar mensaje",
                                Error = ex.Message
                            });
                        }
                    }

                    return Ok(new
                    {
                        Exito = true,
                        Mensaje = "Actividad ejecutada correctamente",
                        IdEjecucion = resultado.IdRegistro,
                        MensajeResultado = mensajeEnvio ?? resultado.Mensaje
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Exito = false,
                        Mensaje = "Error al ejecutar actividad",
                        Error = resultado.Error
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Detalle = ex.InnerException?.Message
                });
            }
        }

        /// <summary>
        /// Metodo privado para enviar correo de actividad automatica.
        /// Usa servicios directamente sin depender de CorreoController ni HttpContext.
        /// </summary>
        private async Task<string> EnviarCorreoActividadAsync(ActividadPendienteDTO actividad)
        {
            // 1. Obtener gestion de contacto y docente
            var gestionContacto = await _unitOfWork.GestionContactoRepository.ObtenerPorIdAsync(actividad.IdGestionContacto);
            if (gestionContacto == null || !gestionContacto.IdClasificacionPersona.HasValue)
            {
                throw new Exception("No se encontro la gestion de contacto o no tiene clasificacion de persona");
            }

            var docente = _unitOfWork.DocentePostulanteRepository.ObtenerDocenteDTOPorIdClasificacionPersona(gestionContacto.IdClasificacionPersona.Value);
            if (docente == null || string.IsNullOrWhiteSpace(docente.Correo))
            {
                throw new Exception("No se encontro el correo del docente");
            }

            // 2. Generar plantilla con etiquetas reemplazadas
            var plantillaGenerada = _gestionDocenteActividadService.GenerarPlantillaDocente(new ReemplazoEtiquetaPlantillaDocenteDTO
            {
                IdGestionContacto = actividad.IdGestionContacto,
                IdPlantilla = actividad.IdPlantilla
            });

            if (string.IsNullOrWhiteSpace(plantillaGenerada.EmailReemplazado.Asunto) ||
                string.IsNullOrWhiteSpace(plantillaGenerada.EmailReemplazado.CuerpoHTML))
            {
                throw new Exception("La plantilla de email generada esta vacia");
            }

            // 3. Obtener datos del asesor remitente
            var personalService = new PersonalService(_unitOfWork);
            var asesor = _unitOfWork.PersonalRepository.FirstById(6205);
            if (asesor == null)
            {
                throw new Exception("No se encontro el asesor sistema con Id 6205");
            }

            // 4. Obtener credenciales SMTP del asesor
            var gmailClienteService = new GmailClienteService(_unitOfWork);
            var credenciales = gmailClienteService.ObtenerClienteCredencial(6205);
            if (credenciales == null)
            {
                throw new Exception("No se encontraron credenciales de correo para el asesor sistema");
            }

            // 5. Preparar datos del correo
            var mailData = new TMKMailDataDTO
            {
                Sender = credenciales.EmailAsesor,
                Recipient = docente.Correo,
                Subject = plantillaGenerada.EmailReemplazado.Asunto,
                Message = plantillaGenerada.EmailReemplazado.CuerpoHTML,
                Cc = "",
                Bcc = "",
                AttachedFiles = null,
                RemitenteC = string.Concat(asesor.Nombres, " ", asesor.Apellidos)
            };

            // 6. Enviar via SMTP sin archivos adjuntos
            var filtroBandejaCorreo = new FiltroBandejaCorreoService(_unitOfWork);
            IList<Microsoft.AspNetCore.Http.IFormFile> sinArchivos = new List<Microsoft.AspNetCore.Http.IFormFile>();
            var rptEnvio = filtroBandejaCorreo.envioEmailAdjuntoOperaciones(
                credenciales.EmailAsesor,
                credenciales.PasswordCorreo,
                mailData,
                sinArchivos
            );

            if (rptEnvio.codigo != "200")
            {
                throw new Exception($"Error SMTP al enviar correo: {rptEnvio.respuesta}");
            }

            // 7. Registrar correo enviado en GmailCorreo
            var gmailCorreoService = new GmailCorreoService(_unitOfWork);
            var gmailCorreo = new GmailCorreo
            {
                IdEtiqueta = 1,
                Asunto = plantillaGenerada.EmailReemplazado.Asunto,
                Fecha = DateTime.Now,
                EmailBody = plantillaGenerada.EmailReemplazado.CuerpoHTML,
                Seen = false,
                Remitente = credenciales.EmailAsesor,
                Cc = "",
                Bcc = "",
                Destinatarios = docente.Correo,
                IdPersonal = asesor.Id,
                IdClasificacionPersona = gestionContacto.IdClasificacionPersona,
                Estado = true,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                UsuarioCreacion = "EJECUCION_MANUAL",
                UsuarioModificacion = "EJECUCION_MANUAL"
            };
            gmailCorreoService.Add(gmailCorreo);

            return $"Email enviado exitosamente a {docente.Correo} - Asunto: {plantillaGenerada.EmailReemplazado.Asunto}";
        }

        /// Autor: Lolo Zaa
        /// Fecha: 06/03/2026
        /// Version: 1.0
        /// <summary>
        /// Metodo privado para enviar WhatsApp de actividad automatica.
        /// Usa WhatsAppMensajeEnviadoApiPlanificacionService directamente.
        /// </summary>
        private async Task<string> EnviarWhatsAppActividadAsync(ActividadPendienteDTO actividad)
        {
            // 1. Obtener gestion de contacto y docente
            var gestionContacto = await _unitOfWork.GestionContactoRepository.ObtenerPorIdAsync(actividad.IdGestionContacto);
            if (gestionContacto == null || !gestionContacto.IdClasificacionPersona.HasValue)
            {
                throw new Exception("No se encontro la gestion de contacto o no tiene clasificacion de persona");
            }

            var docente = _unitOfWork.DocentePostulanteRepository.ObtenerDocenteDTOPorIdClasificacionPersona(gestionContacto.IdClasificacionPersona.Value);
            if (docente == null || string.IsNullOrWhiteSpace(docente.Celular))
            {
                throw new Exception("No se encontro el celular del docente");
            }

            // 2. Obtener IdProveedor desde ClasificacionPersona
            var clasificacionPersona = _unitOfWork.ClasificacionPersonaRepository.FirstById(gestionContacto.IdClasificacionPersona.Value);
            if (clasificacionPersona == null)
            {
                throw new Exception("No se encontro la clasificacion de persona");
            }
            int idProveedor = clasificacionPersona.IdTablaOriginal;

            // 3. Generar plantilla WhatsApp con etiquetas reemplazadas
            var plantillaGenerada = _gestionDocenteActividadService.GenerarPlantillaDocente(new ReemplazoEtiquetaPlantillaDocenteDTO
            {
                IdGestionContacto = actividad.IdGestionContacto,
                IdPlantilla = actividad.IdPlantilla
            });

            if (plantillaGenerada.WhatsAppReemplazado == null ||
                string.IsNullOrWhiteSpace(plantillaGenerada.WhatsAppReemplazado.Plantilla))
            {
                throw new Exception("La plantilla de WhatsApp generada esta vacia");
            }

            // 4. Mapear etiquetas PascalCase a camelCase con indice posicional
            var datosPlantilla = new List<DatosPlantillaWhatsAppDTO>();
            if (plantillaGenerada.WhatsAppReemplazado.ListaEtiquetas != null)
            {
                int indice = 1;
                foreach (var etiqueta in plantillaGenerada.WhatsAppReemplazado.ListaEtiquetas)
                {
                    datosPlantilla.Add(new DatosPlantillaWhatsAppDTO
                    {
                        codigo = indice.ToString(),
                        texto = etiqueta.Texto ?? ""
                    });
                    indice++;
                }
            }

            // 5. Construir y enviar mensaje WhatsApp por plantilla
            var whatsAppService = new WhatsAppMensajeEnviadoApiPlanificacionService(_unitOfWork);
            var parametros = new WhatsAppMensajePlantillaPlaDTO
            {
                WaTo = docente.Celular,
                WaBody = plantillaGenerada.WhatsAppReemplazado.Plantilla,
                WaCaption = null,
                WaTypeMensaje = 1,
                IdPlantilla = actividad.IdPlantilla,
                IdPais = 51, // Peru por defecto
                IdProveedor = idProveedor,
                IdPersonal = 6205, // Asesor sistema
                DatosPlantillaWhatsApp = datosPlantilla
            };

            var resultado = whatsAppService.EnvioMensajePorPlantilla(parametros, "EJECUCION_MANUAL", 6205);

            if (!resultado.Estado)
            {
                throw new Exception($"Error al enviar WhatsApp: {resultado.Mensaje}");
            }

            return $"WhatsApp enviado exitosamente a {docente.Celular} - Plantilla: {plantillaGenerada.WhatsAppReemplazado.Plantilla}";
        }

        /// Autor: Lolo Zaa
        /// Fecha: 05/03/2026
        /// Version: 1.0
        /// <summary>
        /// Marca una actividad manual como completada despues de que el usuario realizo la tarea.
        /// Solo aplica para actividades tipo Manual (IdTipoActividad = 2).
        /// </summary>
        /// <param name="request">Datos de la actividad completada</param>
        /// <returns>Resultado de la operacion</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> MarcarActividadManualCompletada([FromBody] MarcarActividadCompletadaDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Exito = false, Mensaje = "Modelo invalido", Errores = ModelState });

                var resultado = await _gestionContactoService.ActualizarEstadoActividadAsync(new ActualizarEstadoRequestDTO
                {
                    IdActividadDetalleCongelada = request.IdActividadDetalleCongelada,
                    IdDisparadorCongelado = request.IdDisparadorCongelado,
                    CodigoNuevoEstado = "EJECUTADO",
                    MensajeResultado = $"Completada manualmente por {request.UsuarioEjecucion}. {request.Comentario}",
                    UsuarioModificacion = request.UsuarioEjecucion
                });

                if (resultado.Exitoso)
                {
                    return Ok(new
                    {
                        Exito = true,
                        Mensaje = "Actividad marcada como completada",
                        IdEjecucion = resultado.IdRegistro
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Exito = false,
                        Mensaje = resultado.Error
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Detalle = ex.InnerException?.Message
                });
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 05/03/2026
        /// Version: 1.0
        /// <summary>
        /// Adelanta la fecha de ejecucion de una actividad para que Hangfire la procese
        /// en el siguiente ciclo (5 minutos).
        /// </summary>
        /// <param name="request">Datos de la actividad a adelantar</param>
        /// <returns>Resultado de la operacion con la nueva fecha programada</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> AdelantarEjecucionActividad([FromBody] AdelantarActividadDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Exito = false, Mensaje = "Modelo invalido", Errores = ModelState });

                var resultado = await _gestionContactoService.AdelantarFechaActividadAsync(request);

                if (resultado.Exitoso)
                {
                    return Ok(new
                    {
                        Exito = true,
                        Mensaje = "Fecha adelantada. La actividad sera procesada por Hangfire en el proximo ciclo (5 minutos)",
                        NuevaFechaProgramada = resultado.NuevaFecha
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Exito = false,
                        Mensaje = resultado.Error
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Detalle = ex.InnerException?.Message
                });
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 05/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene las actividades que dependen de una actividad especifica.
        /// Busca actividades con disparador OCURRENCIA_PREVIA vinculadas a la misma ocurrencia.
        /// </summary>
        /// <param name="idActividadDetalleCongelada">ID de la actividad a consultar</param>
        /// <returns>Lista de actividades dependientes</returns>
        [HttpGet("[action]/{idActividadDetalleCongelada}")]
        public async Task<IActionResult> ObtenerActividadesDependientes(int idActividadDetalleCongelada)
        {
            try
            {
                var dependientes = await _gestionContactoService.ObtenerActividadesDependientesAsync(idActividadDetalleCongelada);

                return Ok(new
                {
                    Exito = true,
                    TieneDependientes = dependientes.Any(),
                    CantidadDependientes = dependientes.Count,
                    Dependientes = dependientes
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Exito = false,
                    Mensaje = ex.Message,
                    Detalle = ex.InnerException?.Message
                });
            }
        }

    }
}
