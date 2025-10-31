using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
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
    public class ProgramaGeneralProblemaFactorSolucionRespuestaController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IProgramaGeneralProblemaFactorSolucionRespuestaService _programaGeneralProblemaFactorSolucionRespuestaService;
        public ProgramaGeneralProblemaFactorSolucionRespuestaController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _programaGeneralProblemaFactorSolucionRespuestaService = new ProgramaGeneralProblemaFactorSolucionRespuestaService(unitOfWork);
            _tokenManager = tokenManager;
        }

        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult GuardarProblemaClienteSolucion([FromBody] ProgramaGeneralProblemaFactorSolucionRespuestaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _programaGeneralProblemaFactorSolucionRespuestaService.GuardarProblemaClienteSolucion(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }
    }
}
