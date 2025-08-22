using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: ConfigurarWebinarController
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 18/04/2023
    /// <summary>
    /// Gestion de Materiales de Accion
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    //[ServiceFilter(typeof(JwtActionFilter))]
    public class ConfigurarWebinarController : Controller
    {
        private IConfigurarWebinarService _configurarWebinarService;
        public ConfigurarWebinarController(IUnitOfWork unitOfWork)
        {
            _configurarWebinarService = new ConfigurarWebinarService(unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las configuraciones webinar por id pespecifico
        /// </summary>
        /// <param name="idPEspecificoPadre"></param>
        /// <returns></returns>
        [Route("[action]/{idPEspecificoPadre}")]
        [HttpGet]
        public ActionResult<IEnumerable<ConfigurarWebinarDTO>> ObtenerPorIdPespecificoPadre(int idPEspecificoPadre)
        {
            return Ok(_configurarWebinarService.ObtenerPorIdPespecificoPadre(idPEspecificoPadre));
        }
    }
}
