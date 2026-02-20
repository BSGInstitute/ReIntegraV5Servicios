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
    /// Provee dos endpoints GET: lista de docentes con cursos y detalle de docente.
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
    }
}
