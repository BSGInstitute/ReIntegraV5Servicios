using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AspNetUserController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private IConfiguration configuration;
        private ITokenManager _tokenManager;

        public AspNetUserController(IUnitOfWork unitOfWork, IConfiguration configuration, ITokenManager tokenManager)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            _tokenManager = tokenManager;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] UserCredentialsDTO userCred)
        {

            var _servicio = new CustomAuthenticationManagerServiceImpl(configuration, unitOfWork);

            var token = _servicio.Authenticate(userCred.Username, userCred.Password);

            if (token.Excepcion.ExcepcionGenerada == true)
            {
                return Unauthorized(token);
            }
            return Ok(token);
        }
    }
}
