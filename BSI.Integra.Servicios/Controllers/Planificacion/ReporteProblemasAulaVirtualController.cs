using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: ReporteProblemasAulaVirtualController
    /// Autor: Jonathan Caipo
    /// Fecha: 21/04/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión del Reporte Problemas Aula Virtual
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteProblemasAulaVirtualController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReporteProblemasAulaVirtualController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 21/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los combos del reporte de Problemas del Aula Virtual
        /// </summary>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerCombos()
        {
            try
            {
                IReporteProblemasAulaVirtualService reporteProblemasAulaVirtualService = new ReporteProblemasAulaVirtualService(unitOfWork);
                return Ok(reporteProblemasAulaVirtualService.ObtenerCombos());
            }
            catch
            {
                throw;
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 21/04/2023
        /// Version: 1.0
        /// <summary>
        /// Generea reporte de Problemas Aula Virtual
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteProblemasAulaVirtualFiltroDTO filtro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                IReporteProblemasAulaVirtualService reporteProblemasAulaVirtualService = new ReporteProblemasAulaVirtualService(unitOfWork);
                return Ok(reporteProblemasAulaVirtualService.ReporteProblemasAulaVirtual(filtro).OrderByDescending(w => w.Id));
            }
            catch
            {
                throw;
            }
        }
    }
}
