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
        private readonly IGestionDocenteAgendaService _service;

        public GestionDocenteAgendaController(IGestionDocenteAgendaService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtiene la lista de docentes con cursos y flujo asignado.
        /// Solo aparecen docentes que tienen al menos un flujo activo en T_GestionContactoDocenteFlujo.
        /// </summary>
        [HttpGet("[Action]")]
        public IActionResult ObtenerDocentesConCursos()
        {
            try
            {
                var resultado = _service.ObtenerDocentesConCursos();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { exito = false, mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene el detalle completo de un docente: cabecera, flujo y cronogramas con sesiones.
        /// El cronograma con idPEspecifico tiene esPriorizado = true y aparece primero.
        /// </summary>
        [HttpGet("[Action]/{idProveedor}/{idPEspecifico}")]
        public IActionResult ObtenerDetalleDocente(
            int idProveedor,
            int idPEspecifico,
            [FromQuery] int? idGestionContacto)
        {
            try
            {
                var resultado = _service.ObtenerDetalleDocente(idProveedor, idPEspecifico, idGestionContacto);
                if (resultado == null)
                    return NotFound(new { exito = false, mensaje = "No se encontró el docente." });
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { exito = false, mensaje = ex.Message });
            }
        }
    }
}
