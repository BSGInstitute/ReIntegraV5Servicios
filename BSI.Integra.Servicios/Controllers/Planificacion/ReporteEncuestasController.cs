using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: ReporteEncuestaFinalController
    /// Autor: Jonathan Caipo
    /// Fecha: 17/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión del Reporte Encuesta Final
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteEncuestasController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReporteEncuestasController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
		/// Fecha: 22/05/2021
		/// Version: 1.0
        /// <summary>
        /// Generar el reporte de encuestas iniciales segun el filtro ingresado
        /// </summary>
        /// <param name="dto">filtro para la seleccion del reporte por fechainicio, fechafin, nombre y dni</param>
        /// <returns>Lista del reporte encuestas iniciales en un List<ReporteEncuestasInicialesDTO></returns>
        [Route("[Action]")]
        [HttpPost]
        public IActionResult GenerarReporteEncuestaInicial([FromBody] ReporteEncuestasFiltroDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                IReporteEncuestasService reporteEncuestasService = new ReporteEncuestasService(unitOfWork);
                return Ok(reporteEncuestasService.GenerarReporte(dto,1));
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
		/// Fecha: 22/05/2021
		/// Version: 1.0
        /// <summary>
        /// Generar el reporte de encuestas iniciales segun el filtro ingresado
        /// </summary>
        /// <param name="dto">filtro para la seleccion del reporte por fechainicio, fechafin, nombre y dni</param>
        /// <returns>Lista del reporte encuestas iniciales en un List<ReporteEncuestasInicialesDTO></returns>
        [Route("[Action]")]
        [HttpPost]
        public IActionResult GenerarReporteEncuestaIntermedia([FromBody] ReporteEncuestasFiltroDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                IReporteEncuestasService reporteEncuestasService = new ReporteEncuestasService(unitOfWork);
                return Ok(reporteEncuestasService.GenerarReporte(dto,2));
            }
            catch
            {
                throw;
            }
        }
        /// TipoFuncion: POST
		/// Autor: Jonathan Caipo
		/// Fecha: 17/05/2021
		/// Version: 1.0
		/// <summary>
		/// Funcion que trae los datos para el reporte de encuesta final
		/// </summary>
		/// <param name="dto">Datos del filtro que vienen de interfaz</param>
		/// <returns>Retorma una lista del tipo PEspecificoFiltroPGeneralDTO </returns>
		[Route("[Action]")]
        [HttpPost]
        public IActionResult GenerarReporteEncuestaFinal([FromBody] ReporteEncuestasFiltroDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                IReporteEncuestasService reporteEncuestasService = new ReporteEncuestasService(unitOfWork);
                return Ok(reporteEncuestasService.GenerarReporte(dto,3));
            }
            catch
            {
                throw;
            }
        }

        /// TipoFuncion: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 02/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene las versiones de las encuesta
        /// </summary>
        /// <param name="idTipoEncuesta">Datos del filtro que vienen de interfaz</param>
        /// <returns>Retorna una lista del tipo VersionEncuestaDTO </returns>

        [Route("[Action]/{idTipoEncuesta}")]
        [HttpGet]
        public IActionResult ObtenerVersionEncuesta(int idTipoEncuesta)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                IReporteEncuestasService reporteEncuestasService = new ReporteEncuestasService(unitOfWork);
                return Ok(reporteEncuestasService.ObtenerVersionEncuesta(idTipoEncuesta));
            }
            catch
            {
                throw;
            }
        }
    }
}
