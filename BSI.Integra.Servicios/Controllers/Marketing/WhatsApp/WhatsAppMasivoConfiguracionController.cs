using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.CampaniasMailingWhatsapp;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Marketing.WhatsApp
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class WhatsAppMasivoConfiguracionController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;

        public WhatsAppMasivoConfiguracionController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpPost]
        public IActionResult AsignarCantidadDeDataAPersonal()
        {
            return Ok();
        } 
    }
}
