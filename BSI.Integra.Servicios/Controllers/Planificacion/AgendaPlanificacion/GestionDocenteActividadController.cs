using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers.Planificacion.AgendaPlanificacion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class GestionDocenteActividadController : ControllerBase
    {
        private readonly IGestionDocenteActividadService _gestionDocenteActividadService;

        public GestionDocenteActividadController(IGestionDocenteActividadService gestionDocenteActividadService)
        {
            _gestionDocenteActividadService = gestionDocenteActividadService;
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 28/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que inserta una actividad completa con su cabecera, detalles, disparadores y ocurrencias en una sola transacción maestro.
        /// </summary>
        /// <param name="dto">DTO maestro con cabecera, detalles y ocurrencias.</param>
        /// <returns>ActionResult con el Id de la cabecera creada.</returns>
        [HttpPost("InsertarMaestro")]
        public async Task<IActionResult> InsertarMaestro([FromBody] MaestroGestionDocenteActividadDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { Exito = false, Mensaje = "Modelo inválido", Errores = ModelState });

                var idGenerado = await _gestionDocenteActividadService.ProcesarMaestroActividadAsync(dto);

                return Ok(new { Exito = true, Mensaje = "Actividad y jerarquía creadas correctamente", Id = idGenerado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 29/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que inserta una cabecera de actividad docente.
        /// </summary>
        /// <param name="dto">DTO con los datos de la cabecera de actividad.</param>
        /// <returns>ActionResult con el Id de la cabecera creada.</returns>
        [HttpPost("InsertarCabecera")]
        public async Task<IActionResult> InsertarCabecera([FromBody] GestionDocenteActividadCabeceraDTO dto)
        {
            try
            {
                var id = await _gestionDocenteActividadService.InsertarCabeceraAsync(dto);
                return Ok(new { Exito = true, Id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 29/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que inserta un detalle de actividad con su disparador, reglas de tiempo y referencias asociadas.
        /// </summary>
        /// <param name="request">DTO con detalle, disparador, reglas de tiempo y referencias.</param>
        /// <returns>ActionResult con el Id del detalle creado.</returns>
        [HttpPost("InsertarDetalle")]
        public async Task<IActionResult> InsertarDetalle([FromBody] InsertarActividadDetalleRequestDTO request)
        {
            try
            {
                var id = await _gestionDocenteActividadService.InsertarDetalleAsync(request);
                return Ok(new { Exito = true, Id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 29/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que inserta una ocurrencia asociada a un detalle de actividad, incluyendo configuración IA y ejemplos de entrenamiento si aplica.
        /// </summary>
        /// <param name="request">DTO con ocurrencia, configuración IA y ejemplos de entrenamiento.</param>
        /// <returns>ActionResult con el Id de la ocurrencia creada.</returns>
        [HttpPost("InsertarOcurrencia")]
        public async Task<IActionResult> InsertarOcurrencia([FromBody] InsertarOcurrenciaRequestDTO request)
        {
            try
            {
                var id = await _gestionDocenteActividadService.InsertarOcurrenciaAsync(request);
                return Ok(new { Exito = true, Id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Vega
        /// Fecha: 30/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que asocia una actividad cabecera a un flujo docente.
        /// </summary>
        /// <param name="dto">DTO con IdGestionDocenteFlujo e IdGestionDocenteActividadCabecera.</param>
        /// <returns>ActionResult con el Id de la asociación creada.</returns>
        [HttpPost("AsociarAFlujo")]
        public async Task<IActionResult> AsociarAFlujo([FromBody] GestionDocenteActividadCabeceraFlujoDTO dto)
        {
            try
            {
                var id = await _gestionDocenteActividadService.AsociarActividadAFlujoAsync(dto);
                return Ok(new { Exito = true, Id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Jose Vega
        /// Fecha: 30/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que desasocia una actividad cabecera de un flujo docente (eliminación lógica).
        /// </summary>
        /// <param name="id">Identificador de la asociación actividad-flujo.</param>
        /// <param name="usuario">Usuario que realiza la operación.</param>
        /// <returns>ActionResult con el resultado de la operación.</returns>
        [HttpDelete("DesasociarDeFlujo")]
        public async Task<IActionResult> DesasociarDeFlujo(int id, string usuario)
        {
            try
            {
                var rpta = await _gestionDocenteActividadService.DesasociarActividadDeFlujoAsync(id, usuario);
                return Ok(new { Exito = rpta });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 30/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene todas las actividades cabecera asociadas a un flujo específico.
        /// </summary>
        /// <param name="idFlujo">Identificador del flujo docente.</param>
        /// <returns>ActionResult con la lista de GestionDocenteActividadCabeceraFlujoDTO.</returns>
        [HttpGet("ObtenerPorFlujo/{idFlujo}")]
        public async Task<IActionResult> ObtenerPorFlujo(int idFlujo)
        {
            try
            {
                var lista = await _gestionDocenteActividadService.ObtenerActividadesPorFlujoAsync(idFlujo);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 05/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene el catálogo de sesiones docentes activas.
        /// </summary>
        /// <returns>ActionResult con la lista de GestionDocenteSesionDTO.</returns>
        [HttpGet("ObtenerSesiones")]
        public IActionResult ObtenerSesiones()
        {
            try
            {
                var lista = _gestionDocenteActividadService.ObtenerSesiones();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 05/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene todas las ocurrencias activas registradas en el sistema.
        /// </summary>
        /// <returns>ActionResult con la lista de GestionDocenteOcurrenciaDTO.</returns>
        [HttpGet("ObtenerOcurrencias")]
        public IActionResult ObtenerOcurrencias()
        {
            try
            {
                var lista = _gestionDocenteActividadService.ObtenerOcurrencias();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 05/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene el catálogo de niveles de umbral de confianza para configuración IA.
        /// </summary>
        /// <returns>ActionResult con la lista de GestionDocenteConfianzaUmbralNivelDTO.</returns>
        [HttpGet("ObtenerConfianzaUmbralNiveles")]
        public IActionResult ObtenerConfianzaUmbralNiveles()
        {
            try
            {
                var lista = _gestionDocenteActividadService.ObtenerConfianzaUmbralNiveles();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 05/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene el catálogo de tipos de ocurrencia (Manual, Automático, Warm).
        /// </summary>
        /// <returns>ActionResult con la lista de GestionDocenteOcurrenciaTipoDTO.</returns>
        [HttpGet("ObtenerOcurrenciaTipos")]
        public IActionResult ObtenerOcurrenciaTipos()
        {
            try
            {
                var lista = _gestionDocenteActividadService.ObtenerOcurrenciaTipos();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 05/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene el catálogo de referencias de tiempo (Antes de sesión, Después de sesión).
        /// </summary>
        /// <returns>ActionResult con la lista de GestionDocenteReferenciaTiempoDTO.</returns>
        [HttpGet("ObtenerReferenciasTiempo")]
        public IActionResult ObtenerReferenciasTiempo()
        {
            try
            {
                var lista = _gestionDocenteActividadService.ObtenerReferenciasTiempo();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 07/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene el catálogo de tipos de actividad detalle.
        /// </summary>
        /// <returns>ActionResult con la lista de GestionDocenteActividadDetalleTipoDTO.</returns>
        [HttpGet("ObtenerActividadDetalleTipos")]
        public IActionResult ObtenerActividadDetalleTipos()
        {
            try
            {
                var lista = _gestionDocenteActividadService.ObtenerActividadDetalleTipos();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 07/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene el catálogo de modos de marcado de ocurrencia (Manual, Automático, Warm).
        /// </summary>
        /// <returns>ActionResult con la lista de GestionDocenteModoMarcadoDTO.</returns>
        [HttpGet("ObtenerModosMarcado")]
        public IActionResult ObtenerModosMarcado()
        {
            try
            {
                var lista = _gestionDocenteActividadService.ObtenerModosMarcado();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 11/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene el catálogo de tipos de disparador de flujo (1ra Actividad, Ocurrencia Anterior, Cronograma).
        /// </summary>
        /// <returns>ActionResult con la lista de GestionDocenteDisparadorFlujoTipoDTO.</returns>
        [HttpGet("ObtenerDisparadorFlujoTipos")]
        public IActionResult ObtenerDisparadorFlujoTipos()
        {
            try
            {
                var lista = _gestionDocenteActividadService.ObtenerDisparadorFlujoTipos();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 13/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene los tipos de disparador de flujo con sus catálogos de configuración asociados:
        /// Tipo 1 solo id y nombre, Tipo 2 incluye unidades de tiempo y ocurrencias, Tipo 3 incluye momentos y unidades de tiempo.
        /// </summary>
        /// <returns>ActionResult con la lista de objetos tipados por tipo de disparador.</returns>
        [HttpGet("ObtenerDisparadorFlujoTiposConfiguracion")]
        public IActionResult ObtenerDisparadorFlujoTiposConfiguracion()
        {
            try
            {
                var lista = _gestionDocenteActividadService.ObtenerDisparadorFlujoTiposConfiguracion();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 07/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene el catálogo de medios de comunicación activos.
        /// </summary>
        /// <returns>ActionResult con la lista de GestionDocenteMedioComunicacionDTO.</returns>
        [HttpGet("ObtenerMediosComunicacion")]
        public IActionResult ObtenerMediosComunicacion()
        {
            try
            {
                var lista = _gestionDocenteActividadService.ObtenerMediosComunicacion();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 07/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene el catálogo de plantillas de medio de comunicación con sus plantillas y medios asociados.
        /// </summary>
        /// <returns>ActionResult con la lista de GestionDocentePlantillaMedioComunicacionDTO.</returns>
        [HttpGet("ObtenerPlantillasMedioComunicacion")]
        public IActionResult ObtenerPlantillasMedioComunicacion()
        {
            try
            {
                var lista = _gestionDocenteActividadService.ObtenerPlantillasMedioComunicacion();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 16/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene las plantillas de medio de comunicación filtradas por un medio de comunicación específico.
        /// </summary>
        /// <param name="idMedioComunicacion">Identificador del medio de comunicación.</param>
        /// <returns>ActionResult con la lista filtrada de GestionDocentePlantillaMedioComunicacionDTO.</returns>
        [HttpGet("ObtenerPlantillasPorMedioComunicacion/{idMedioComunicacion}")]
        public IActionResult ObtenerPlantillasPorMedioComunicacion(int idMedioComunicacion)
        {
            try
            {
                var lista = _gestionDocenteActividadService.ObtenerPlantillasMedioComunicacionPorMedioComunicacion(idMedioComunicacion);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 13/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene el catálogo de unidades de tiempo (Minutos, Horas, Días).
        /// </summary>
        /// <returns>ActionResult con la lista de GestionDocenteUnidadTiempoDTO.</returns>
        [HttpGet("ObtenerUnidadesTiempo")]
        public IActionResult ObtenerUnidadesTiempo()
        {
            try
            {
                var lista = _gestionDocenteActividadService.ObtenerUnidadesTiempo();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 11/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene todos los disparadores activos con sus reglas de tiempo ensambladas por tipo
        /// (Tiempo Fijo, Ocurrencia Anterior, Cronograma) usando DTOs específicos para eliminar campos null.
        /// </summary>
        /// <returns>ActionResult con la lista de objetos tipados por tipo de disparador.</returns>
        [HttpGet("ObtenerDisparadorReglaTiempo")]
        public IActionResult ObtenerDisparadorReglaTiempo()
        {
            try
            {
                var lista = _gestionDocenteActividadService.ObtenerDisparadorReglaTiempo();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 06/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene la actividad cabecera completa con toda su jerarquía: detalles, disparadores,
        /// reglas de tiempo, sesiones, ocurrencias, configuración IA y ejemplos de entrenamiento.
        /// </summary>
        /// <param name="id">Identificador de la actividad cabecera.</param>
        /// <returns>ActionResult con ActividadCabeceraCompletaDTO.</returns>
        [HttpGet("ObtenerActividadCompleta/{id}")]
        public async Task<IActionResult> ObtenerActividadCompleta(int id)
        {
            try
            {
                var resultado = await _gestionDocenteActividadService.ObtenerActividadCabeceraCompletaAsync(id);
                if (resultado == null)
                    return NotFound(new { Exito = false, Mensaje = "No se encontró la actividad cabecera" });

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        [HttpGet("ObtenerClasificacionTipos")]
        public IActionResult ObtenerClasificacionTipos()
        {
            try { var lista = _gestionDocenteActividadService.ObtenerClasificacionTipos(); return Ok(lista); }
            catch (Exception ex) { return BadRequest(new { Exito = false, Mensaje = ex.Message }); }
        }

        [HttpGet("ObtenerEjemplosEntrenamiento/{idIaConfiguracion}")]
        public IActionResult ObtenerEjemplosEntrenamiento(int idIaConfiguracion)
        {
            try { var lista = _gestionDocenteActividadService.ObtenerEjemplosEntrenamientoPorConfiguracion(idIaConfiguracion); return Ok(lista); }
            catch (Exception ex) { return BadRequest(new { Exito = false, Mensaje = ex.Message }); }
        }

        [HttpPost("InsertarEjemploEntrenamiento")]
        public async Task<IActionResult> InsertarEjemploEntrenamiento([FromBody] InsertarEjemploEntrenamientoRequestDTO request)
        {
            try { var id = await _gestionDocenteActividadService.InsertarEjemploEntrenamientoAsync(request); return Ok(new { Exito = true, Id = id }); }
            catch (Exception ex) { return BadRequest(new { Exito = false, Mensaje = ex.Message }); }
        }

        [HttpPut("ActualizarEjemploEntrenamiento")]
        public async Task<IActionResult> ActualizarEjemploEntrenamiento([FromBody] ActualizarEjemploEntrenamientoRequestDTO request)
        {
            try { var rpta = await _gestionDocenteActividadService.ActualizarEjemploEntrenamientoAsync(request); return Ok(new { Exito = rpta }); }
            catch (Exception ex) { return BadRequest(new { Exito = false, Mensaje = ex.Message }); }
        }

        [HttpDelete("EliminarEjemploEntrenamiento")]
        public async Task<IActionResult> EliminarEjemploEntrenamiento(int id, string usuario)
        {
            try { var rpta = await _gestionDocenteActividadService.EliminarEjemploEntrenamientoAsync(id, usuario); return Ok(new { Exito = rpta }); }
            catch (Exception ex) { return BadRequest(new { Exito = false, Mensaje = ex.Message }); }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 19/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que genera una plantilla de email o WhatsApp para docentes, reemplazando las etiquetas
        /// con datos reales del docente, coordinador y curso asociado a la gestión de contacto.
        /// </summary>
        /// <param name="idGestionContacto">Identificador de la gestión de contacto.</param>
        /// <param name="idPlantilla">Identificador de la plantilla a procesar.</param>
        /// <returns>ActionResult con PlantillaEmailMandrillDTO o PlantillaWhatsAppCalculadoDTO según el tipo de plantilla.</returns>
        [HttpGet("GenerarPlantillaDocente/{idGestionContacto}/{idPlantilla}")]
        public IActionResult GenerarPlantillaDocente(int idGestionContacto, int idPlantilla)
        {
            try
            {
                var resultado = _gestionDocenteActividadService.GenerarPlantillaDocente(new ReemplazoEtiquetaPlantillaDocenteDTO
                {
                    IdGestionContacto = idGestionContacto,
                    IdPlantilla = idPlantilla
                });

                return Ok(new
                {
                    EmailReemplazado = resultado.EmailReemplazado,
                    WhatsAppReemplazado = resultado.WhatsAppReemplazado
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 25/02/2026
        /// Versión: 2.0
        /// <summary>
        /// Obtiene las plantillas disponibles para la agenda de planificación docente
        /// según el módulo, tipo de plantilla y área de trabajo enviados desde el front.
        /// </summary>
        /// <param name="idModuloSistemaV5">Id del módulo del sistema V5.</param>
        /// <param name="idPlantillaBase">Id del tipo de plantilla (2=Email, 8=WhatsApp).</param>
        /// <param name="idPersonalAreaTrabajo">Id del área de trabajo del personal logueado.</param>
        /// <returns>Lista de PlantillaDisponiblePlanificacionDTO.</returns>
        [HttpGet("ObtenerPlantillasPlanificacion/{idModuloSistemaV5}/{idPlantillaBase}/{idPersonalAreaTrabajo}")]
        public IActionResult ObtenerPlantillasPlanificacion(int idModuloSistemaV5, int idPlantillaBase, int idPersonalAreaTrabajo)
        {
            try
            {
                var lista = _gestionDocenteActividadService.ObtenerPlantillasPlanificacion(idModuloSistemaV5, idPlantillaBase, idPersonalAreaTrabajo);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }
    }
}
