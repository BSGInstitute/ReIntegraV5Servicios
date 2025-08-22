using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Comercial
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteContactabilidadTresCxController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReporteContactabilidadTresCxController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReportev2([FromBody] ReporteContactabilidadFiltroAlternoDTO reporteContactabilidadFiltro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var servicioReporte = new ReporteService(unitOfWork);
            var resultado = servicioReporte.ReporteContactabilidadV2TresCx(reporteContactabilidadFiltro);
            return Ok(resultado);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 14/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de contactabilidad alterno 3cx desglosado
        /// </summary>
        /// <returns> objeto DTO : ReporteContactabilidadCombosDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReportev2Alterno([FromBody] ReporteContactabilidadFiltroAlternoDTO reporteContactabilidadFiltro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IReporteService servicioReporte = new ReporteService(unitOfWork);
            var resultado = servicioReporte.ReporteContactabilidadV2TresCxAlterno(reporteContactabilidadFiltro);
            return Ok(resultado);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 08/07/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de llamadas entrantes
        /// </summary>
        /// <returns> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteLlamadaEntrante([FromBody] FiltroReporteLlamadaEntranteDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IReporteService servicioReporte = new ReporteService(unitOfWork);
            var resultado = servicioReporte.ObtenerReporteLlamadaEntrante(filtro);
            return Ok(resultado);
        }
    }
}
