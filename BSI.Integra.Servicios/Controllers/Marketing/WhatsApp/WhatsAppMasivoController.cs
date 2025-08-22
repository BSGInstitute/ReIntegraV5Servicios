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

    public class WhatsAppMasivoController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public WhatsAppMasivoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpPost]
        public void AsignarCantidadDeDataAPersonal()
        {

        }
        [Route("idprioridad")]
        [HttpGet]
        public IActionResult ObtenerTotalDeDataPorPrioridad(int idprioridad)
        {
            return Ok();
        }

    }
}
