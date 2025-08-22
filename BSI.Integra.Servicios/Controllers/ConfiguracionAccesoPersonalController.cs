using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ConfiguracionAccesoPersonalController
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 18/07/2023
    /// <summary>
    /// Gestión de ConfiguracionAccesoPersonal
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ConfiguracionAccesoPersonalController : ControllerBase
    {
        private IConfiguracionAccesoPersonalService _configuracionAccesoPersonalService;
        public ConfiguracionAccesoPersonalController(IConfiguracionAccesoPersonalService configuracionAccesoPersonalService)
        {
            _configuracionAccesoPersonalService = configuracionAccesoPersonalService;
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_ConfiguracionAccesoPersonal por id personal.
        /// </summary>
        /// <returns> Lista ConfiguracionAccesoPersonal </returns>
        [HttpGet("[action]/{idPersonal}")]
        public IActionResult ObtenerPorIdPersonal(int idPersonal)
        {
            var resultado = _configuracionAccesoPersonalService.ObtenerPorIdPersonal(idPersonal);
            return Ok(resultado);
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_ConfiguracionAccesoPersonal para id personal id modulo.
        /// </summary>
        /// <returns> ConfiguracionAccesoPersonal </returns>
        [HttpGet("[action]/{idPersonal}/{idModulo}")]
        public IActionResult ObtenerPorIdPersonalIdModulo(int idPersonal, int idModulo)
        {
            var resultado = _configuracionAccesoPersonalService.ObtenerPorIdPersonalIdModulo(idPersonal, idModulo);
            return Ok(resultado);
        }
    }
}
