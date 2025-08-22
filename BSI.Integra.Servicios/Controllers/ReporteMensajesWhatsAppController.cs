using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteMensajesWhatsAppController
    /// Autor: Gilmer Quispe
    /// Fecha: 22/09/2022
    /// <summary>
    /// Gestión Reporte de Cambio de Fase
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteMensajesWhatsAppController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReporteMensajesWhatsAppController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteMensajesWhatsAppFiltrosDTO Json)
        {
            try
            {
                var reporte = new ReporteMensajesWhatsAppService(unitOfWork);
                var result = reporte.ObtenerReporteMensajesWhatsApp(Json);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        //[Route("[action]")]
        //[HttpPost]
        //public ActionResult GenerarReporteMensajesMasivos([FromBody] ReporteMensajesWhatsAppFiltrosDTO Json)
        //{
        //    try
        //    {
        //        ReporteMensajesWhatsAppService reporte = new ReporteMensajesWhatsAppService();
        //        var result = reporte.GenerarReporteMensajesMasivos(Json);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        /// <summary>
        /// General reporte de mensajes WhatsApp Masivos por area
        /// </summary>
        /// <param name="Json">Objeto de clase ReporteMensajesWhatsAppPorAreaFiltrosDTO</param>
        /// <returns>Response 200 con lista de objetos de clase ReporteWhatsAppEnvioMasivoDTO, response 400</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarReporteMensajesMasivosPorArea([FromBody] ReporteMensajesWhatsAppPorAreaFiltrosDTO Json)
        {
            try
            {
                var reporte = new ReporteMensajesWhatsAppService(unitOfWork);
                var resultado = reporte.GenerarReporteMensajesMasivosPorArea(Json);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// General reporte de mensajes WhatsApp Masivos por area
        /// </summary>
        /// <param name="Json">Objeto de clase ReporteWhatsAppMasivoFiltrosDTO</param>
        /// <returns>Response 200 con lista de objetos de clase ReporteWhatsAppEnvioMasivoDTO, response 400</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarReporteMensajesMasivosConjuntoLista([FromBody] ReporteWhatsAppMasivoFiltrosDTO Json)
        {
            try
            {
                var reporte = new ReporteMensajesWhatsAppService(unitOfWork);
                var resultado = reporte.GenerarReporteMensajesMasivosConjuntoLista(Json);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
