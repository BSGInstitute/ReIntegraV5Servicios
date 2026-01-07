using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class MontoPagoLogController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IMontoPagoLogService _montoPagoLogService;
        public MontoPagoLogController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _montoPagoLogService = new MontoPagoLogService(unitOfWork);
            _tokenManager = tokenManager;
        }
        [HttpPost("[action]")]
        public IActionResult ObtenerReporteMontoPagoHistorico([FromBody] FiltroMontoPagoHistoricoDTO filtro)
        {
            var resultado = _montoPagoLogService.ObtenerReporteMontoPagoHistorico(filtro);
            return Ok(resultado);
        }
    }
}
