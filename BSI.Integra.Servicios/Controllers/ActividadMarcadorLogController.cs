using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Web;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ActividadMarcadorLogController
    /// Autor: Gilmer Quispe.
    /// Fecha: 07/09/2022
    /// <summary>
    /// Gestión general de la tabla T_ActividadMarcadorLog
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ActividadMarcadorLogController : Controller
    {

        private IActividadMarcadorLogService _actividadMarcadorLogService;
        public ActividadMarcadorLogController(IUnitOfWork unitOfWork)
        {
            _actividadMarcadorLogService = new ActividadMarcadorLogService(unitOfWork);
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 26/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene ActividadMarcadorLog por idActividadDetalle e idOportunidad
        /// </summary>
        /// <returns> </returns>
        [Route("[action]/{idActividadDetalle}/{idOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerPorIdActividadDetalleIdOportunidad(int idActividadDetalle, int idOportunidad)
        {
            try
            {
                var resultado = _actividadMarcadorLogService.ObtenerPorIdActividadDetalleIdOportunidad(idActividadDetalle, idOportunidad);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 26/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene ActividadMarcadorLog por idActividadDetalle e idOportunidad
        /// </summary>
        /// <returns> </returns>
        [Route("[action]/{usuario}")]
        [HttpPost]
        public ActionResult GuardarActividadMarcadorLog([FromBody] ActividadMarcadorLogDTO jsonDTO, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resultado = _actividadMarcadorLogService.GuardarActividadMarcadorLog(jsonDTO, usuario);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
