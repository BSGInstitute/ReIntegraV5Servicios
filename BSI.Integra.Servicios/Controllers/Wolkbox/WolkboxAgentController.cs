using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.Wolkbox.WolkboxAgent;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion.Wolkbox;
using BSI.Integra.Aplicacion.Servicios.Service.Interface.Wolkbox;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BSI.Integra.Servicios.Controllers.Wolkbox
{
    /// Controlador: WolkboxAgentController
    /// Autor: Flavio R.
    /// Fecha: 16/05/2024
    /// <summary>
    /// Gestión de WolkboxAgent
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class WolkboxAgentController : ControllerBase
    {
        private IWolkboxAgentService _wolkboxAgentService;
        private ITokenManager _tokenManager;

        public WolkboxAgentController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _wolkboxAgentService = new WolkboxAgentService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: POST
        /// Autor: Flavio R.
        /// Fecha: 13/05/2024
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns>Resultado Colgar</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Colgar()
        {
            var respuesta = await _wolkboxAgentService.Colgar(_tokenManager.IdPersonal);
            if (respuesta.statusCode == HttpStatusCode.OK)
            {
                return Ok(respuesta.resultado);
            }
            else
            {
                return Conflict(respuesta.resultado);
            }
        }
        /// Tipo Función: POST
        /// Autor: Flavio R.
        /// Fecha: 13/05/2024
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <returns>Resultado Marcar</returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Marcar([FromBody] WolkboxMarcarDTO model)
        {
            var respuesta = await _wolkboxAgentService.Marcar(_tokenManager.IdPersonal, model);
            if (respuesta.statusCode == HttpStatusCode.OK)
            {
                return Ok(respuesta.resultado);
            }
            else
            {
                return Conflict(respuesta.resultado);
            }
        }
    }
}
