using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BSI.Integra.Servicios.Controllers.Planificacion.AgendaPlanificacion
{
    /// Autor: Joseph Llanque
    /// Fecha: 20/02/2026
    /// Versión: 1.0
    /// <summary>
    /// Controlador para la gestión de la agenda de docentes.
    /// Provee endpoints GET para: lista de docentes con cursos, detalle de docente,
    /// tabs configurados desde BD, actividades de todos los tabs y actividades por tab específico.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsVista")]
    public class GestionDocenteAgendaController : ControllerBase
    {
        private readonly IGestionDocenteAgendaService _gestionDocenteAgendaService;

        public GestionDocenteAgendaController(IGestionDocenteAgendaService gestionDocenteAgendaService)
        {
            _gestionDocenteAgendaService = gestionDocenteAgendaService;
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 19/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene la lista de docentes que tienen cursos asignados,
        /// con su respectivo curso y personal asignado.
        /// </summary>
        /// <returns>ActionResult con la lista de DocenteConCursoDTO.</returns>
        [HttpGet("ObtenerDocentesConCursos")]
        public IActionResult ObtenerDocentesConCursos()
        {
            try
            {
                var lista = _gestionDocenteAgendaService.ObtenerDocentesConCursos();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Vega
        /// Fecha: 19/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint que obtiene el detalle completo de un docente: datos personales (nombre, celular,
        /// correo, personal asignado, país, ciudad), flujo asignado y todos sus cronogramas con sesiones.
        /// El curso indicado por idPEspecifico se lista en primer lugar.
        /// </summary>
        /// <param name="idProveedor">Identificador del docente/proveedor.</param>
        /// <param name="idPEspecifico">Identificador del curso a priorizar en la lista de cronogramas.</param>
        /// <param name="idGestionContacto">Identificador opcional del GestionContacto para obtener el flujo asignado.</param>
        /// <returns>ActionResult con DocenteAgendaDetalleDTO.</returns>
        [HttpGet("ObtenerDetalleDocente/{idProveedor}/{idPEspecifico}")]
        public IActionResult ObtenerDetalleDocente(int idProveedor, int idPEspecifico, [FromQuery] int? idGestionContacto)
        {
            try
            {
                var detalle = _gestionDocenteAgendaService.ObtenerDetalleDocente(idProveedor, idPEspecifico, idGestionContacto);
                if (detalle == null) return NotFound(new { Exito = false, Mensaje = "No se encontró el docente." });
                return Ok(detalle);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 24/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuración de todos los tabs de agenda activos para un área de trabajo.
        /// </summary>
        /// <param name="codigoAreaTrabajo">Código del área de trabajo (ej: "PLA").</param>
        /// <returns>ActionResult con lista de AgendaTabConfiguracionPlanificacionAlternoDTO.</returns>
        [HttpGet("ObtenerTabsConfigurados/{codigoAreaTrabajo}")]
        public IActionResult ObtenerTabsConfigurados(string codigoAreaTrabajo)
        {
            try
            {
                var tabs = _gestionDocenteAgendaService.ObtenerTabsConfigurados(codigoAreaTrabajo);
                return Ok(tabs);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 24/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las actividades de todos los tabs configurados, agrupadas por nombre de tab.
        /// Si idAsesor = 0 no filtra por personal asignado.
        /// </summary>
        /// <param name="codigoAreaTrabajo">Código del área de trabajo.</param>
        /// <param name="idAsesor">ID del personal asignado; 0 para obtener todos.</param>
        /// <returns>ActionResult con Dictionary NombreTab → lista de ActividadAgendaPlanificacionDTO.</returns>
        [HttpGet("ObtenerActividades/{codigoAreaTrabajo}/{idAsesor}")]
        public IActionResult ObtenerActividades(string codigoAreaTrabajo, int idAsesor)
        {
            try
            {
                var actividades = _gestionDocenteAgendaService.ObtenerActividades(idAsesor, codigoAreaTrabajo);
                return Ok(actividades);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }

        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 24/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Carga las actividades de un tab específico por su ID y retorna la cantidad total.
        /// Si idAsesor = 0 no filtra por personal asignado.
        /// </summary>
        /// <param name="idTab">ID del tab (T_AgendaTabConfiguracionPlanificacion.Id).</param>
        /// <param name="codigoAreaTrabajo">Código del área de trabajo.</param>
        /// <param name="idAsesor">ID del personal asignado; 0 para obtener todos.</param>
        /// <returns>ActionResult con CargarActividadPorTabResultadoDTO (ActividadesAgenda + Cantidad).</returns>
        [HttpGet("CargarActividadPorTab/{idTab}/{codigoAreaTrabajo}/{idAsesor}")]
        public IActionResult CargarActividadPorTab(int idTab, string codigoAreaTrabajo, int idAsesor)
        {
            try
            {
                var resultado = _gestionDocenteAgendaService.CargarActividadSeleccionadaPorFiltro(idTab, codigoAreaTrabajo, idAsesor);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Exito = false, Mensaje = ex.Message });
            }
        }
    }
}
