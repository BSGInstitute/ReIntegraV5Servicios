using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using Microsoft.Extensions.DependencyInjection;

namespace BSI.Integra.Servicios.Controllers.Marketing.WhatsApp
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]

    public class WhatsAppEnvioAutomaticoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public WhatsAppEnvioAutomaticoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //[Route("[Action]")]
        //[HttpGet]
        //public ActionResult EjecutarCampaniaGeneralEnvioWhatsApp()
        //{
        //    try
        //    {
        //        IWhatsAppEnvioAutomaticoService Service = new WhatsAppEnvioAutomaticoService(unitOfWork);
        //        return Ok(Service.EjecutarCampaniaGeneralEnvioWhatsApp());
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        [Route("[Action]")]
        [HttpGet]
        public ActionResult EjecutarCampaniaGeneralEnvioWhatsAppBoton()
        {
            try
            {
                IWhatsAppEnvioAutomaticoService Service = new WhatsAppEnvioAutomaticoService(unitOfWork);
                return Ok(Service.EjecutarCampaniaGeneralEnvioWhatsAppBoton());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [Route("EjecutarCampaniaGeneralEnvioWhatsAppAsync")]
        [HttpGet]
        public  async  Task<ActionResult> EjecutarCampaniaGeneralEnvioWhatsAppAsync()
        {
            try
            {
                IWhatsAppEnvioAutomaticoService Service = new WhatsAppEnvioAutomaticoService(unitOfWork);
                var resultado = await Service.EjecutarCampaniaGeneralEnvioWhatsAppAsync();
                return Ok(resultado);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult EnvioWhatsappChatBot(WhatsAppChatBotDTO datos)
        {
            try
            {
                IWhatsAppEnvioAutomaticoService Service = new WhatsAppEnvioAutomaticoService(unitOfWork);
                return Ok(Service.EnvioWhatsappChatBot(datos));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerReporteMensajesEnviadosErroneos([FromBody] FiltroMensajesEnviadosErroneosDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var reporte = new WhatsAppEnvioAutomaticoService(unitOfWork);
                var listado = reporte.ObtenerReporteMensajesEnviadosErroneos(filtro);
              
                return Ok(listado);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }



    }
}
