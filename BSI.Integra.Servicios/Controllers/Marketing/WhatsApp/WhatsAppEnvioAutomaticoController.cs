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
        private readonly IServiceScopeFactory _scopeFactory;

        public WhatsAppEnvioAutomaticoController(IUnitOfWork unitOfWork, IServiceScopeFactory scopeFactory)
        {
            this.unitOfWork = unitOfWork;
            _scopeFactory = scopeFactory;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult EjecutarCampaniaGeneralEnvioWhatsApp()
        {
            try
            {
                IWhatsAppEnvioAutomaticoService Service = new WhatsAppEnvioAutomaticoService(unitOfWork);
                return Ok(Service.EjecutarCampaniaGeneralEnvioWhatsApp());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        /// <summary>
        /// Ejecuta la campaña general de envío de mensajes de WhatsApp de forma asíncrona en segundo plano.
        /// </summary>
        /// <returns>OK si el proceso fue iniciado.</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult EjecutarCampaniaGeneralEnvioWhatsAppAuto()
        {
            try
            {
                Task.Run(async () =>
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var unitOfWorkEnScope = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                        var service = new WhatsAppEnvioAutomaticoService(unitOfWorkEnScope);

                        await service.EjecutarCampaniaGeneralEnvioWhatsAppAuto();
                    }
                });

                return Ok("Campaña de envío de WhatsApp iniciada en segundo plano. Verifique los logs para el estado de envíos individuales.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al iniciar la campaña de WhatsApp: {ex.Message}");
            }
        }

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
