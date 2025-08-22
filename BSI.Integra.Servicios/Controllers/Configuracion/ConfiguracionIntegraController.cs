using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Finanzas
{
    /// Controlador: ConfiguracionIntegraController
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 31/01/2024
    /// <summary>
    /// Gestión de ConfiguracionIntegra
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ConfiguracionIntegraController : ControllerBase
    {
        private IConfiguracionIntegraService _configuracionIntegraService;
        public ConfiguracionIntegraController(IConfiguracionIntegraService configuracionIntegraService)
        {
            _configuracionIntegraService = configuracionIntegraService;
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/01/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el estado de validacion de ips
        /// </summary>
        /// <returns> bool => estado validacion ip </returns>
        [AllowAnonymous]
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerEstadoValidacionIp()
        {
            var resultado = _configuracionIntegraService.ObtenerEstadoValidacionIp();
            var apis = _configuracionIntegraService.ObtenerApisValidacionIp();
            return Ok(new
            {
                Estado = resultado,
                Apis = apis
            });
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/01/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las apis para la validacion por ips
        /// </summary>
        /// <returns> Lista de Apis </returns>
        [AllowAnonymous]
        [Route("[Action]")]
        [HttpGet]
        public ActionResult<List<ClaveValorDTO>> ObtenerApisValidacionIp()
        {
            var resultado = _configuracionIntegraService.ObtenerApisValidacionIp();
            return Ok(resultado);

        }
    }
}
