using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ProgramaGeneralProblemaDetalleController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IProgramaGeneralProblemaDetalleService _programaGeneralProblemaDetalleService;
        public ProgramaGeneralProblemaDetalleController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _programaGeneralProblemaDetalleService = new ProgramaGeneralProblemaDetalleService(unitOfWork);
            _tokenManager = tokenManager;
        }
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult Insertar([FromBody] ProgramaGeneralProblemaDetalleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _programaGeneralProblemaDetalleService.Insertar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }

        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] ProgramaGeneralProblemaDetalleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _programaGeneralProblemaDetalleService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }

        [HttpGet("[action]/{idPGeneral}")]
        public IActionResult Obtener(int idPGeneral)
        {
            var resultado = _programaGeneralProblemaDetalleService.Obtener(idPGeneral);
            return Ok(resultado);
        }

    }
}
