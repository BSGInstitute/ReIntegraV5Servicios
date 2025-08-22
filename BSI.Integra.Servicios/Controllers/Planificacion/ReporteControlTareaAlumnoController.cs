using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: ReporteControlTareaAlumnoController
    /// Autor: Jonathan Caipo
    /// Fecha: 03/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión del Reporte Control de Tareas de Alumnos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteControlTareaAlumnoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReporteControlTareaAlumnoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
		/// Autor: Jonathan Caipo
		/// Fecha: 04/05/2023
		/// Version: 1.0
		/// <summary>
		/// Generar el reporte de los trabajos de pares
		/// </summary>
		/// <param name="filtroReporte">Filtro para el reporte de los trabajos de pares  </param>
		/// <returns>El reporte retorna una Lista List<TrabajoDeParesDTO></returns>
		[Route("[action]")]
        [HttpPost]
        public IActionResult GenerarReporteControlTareaAlumno([FromBody] ReporteControlTareaAlumnoFiltroDTO filtroReporte)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                IReporteControlTareaAlumnoService reporteControlTareaAlumnoService = new ReporteControlTareaAlumnoService(unitOfWork);
                return Ok(reporteControlTareaAlumnoService.GenerarReporteControlTareaAlumno(filtroReporte));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
		/// Autor: Jonathan Caipo
		/// Fecha: 05/05/2023
		/// Versión: 1.0
		/// <summary>
		/// Actualiza el encargado de revision de trabajo de Pares 
		/// </summary>
		/// <param name="datos">Datos para acutalizar </param>
		/// <returns>El reporte retorna una Lista List<TrabajoDeParesDTO></returns>
		[Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarPersonaCalificacionControlTareaAlumno([FromBody] ReporteControlTareaAlumnoActualizacionDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                IReporteControlTareaAlumnoService reporteControlTareaAlumnoService = new ReporteControlTareaAlumnoService(unitOfWork);
                var resultado = reporteControlTareaAlumnoService.ActualizarPersonaCalificacionControlTareaAlumno(dto);
                return Ok(new { nombreAlumnoResponsableRevision = resultado, esDocente = true });
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
		/// Autor: Jonathan Caipo
		/// Fecha: 11/07/2023
		/// Versión: 1.0
		/// <summary>
		/// Actualiza la nota de la tarea
		/// </summary>
		/// <param name="dto">Datos para acutalizar </param>
		/// <returns> El reporte retorna una Lista List<TrabajoDeParesDTO> </returns>
		[Route("[action]")]
        [Authorize]
        [HttpPost]
        public IActionResult ActualizarNota([FromBody] NotaDTO dto)
        {
            try
            {
                IReporteControlTareaAlumnoService reporteControlTareaAlumnoService = new ReporteControlTareaAlumnoService(unitOfWork);
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                return Ok(reporteControlTareaAlumnoService.ActualizarNota(dto, registroClaimToken.UserName));
            }
            catch
            {
                throw;
            }
        }
    }
}
