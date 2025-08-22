using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteActividadesRealizadas
    /// Autor: Jonathan Caipo
    /// Fecha: 11/01/2023
    /// <summary>
    /// Gestión Reporte: Reporte de Actividades Realizadas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReportePendienteController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReportePendienteController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 11/01/2023
        /// <summary>
        /// Obtiene combo de pendientes.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns> DTO: comboPendiente - ComboPendienteDTO </returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosPendientes(int idPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ReportePendienteService reportePendienteService = new ReportePendienteService(unitOfWork);
                var comboPendiente = reportePendienteService.ObtenerCombosPendientes(idPersonal);                
                return Ok(comboPendiente);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 11/01/2023
        /// <summary>
        /// Genera reporte general.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns> DTO: reporte - ReportePendienteCompuestoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReportePendienteFiltroDTO filtroPendiente)
        {
            try
            {
                ReportePendienteService reportePendienteService = new ReportePendienteService(unitOfWork);
                var reporte = reportePendienteService.GenerarReporte(filtroPendiente);          
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 11/01/2023
        /// <summary>
        /// Genera Reporte detallado.
        /// </summary>
        /// <param name="filtroPendiente"></param>
        /// <returns> DTO: reporte - ReportePendienteCompuestoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteDetalles([FromBody] ReportePendienteFiltroDTO filtroPendiente)
        {
            try
            {
                ReportePendienteService reportePendienteService = new ReportePendienteService(unitOfWork);
                var reporte = reportePendienteService.GenerarReporteDetalles(filtroPendiente);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}