using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: ConfigurarEvaluacionTrabajoController
    /// Autor: Gilmer Quispe
    /// Fecha: 22/09/2022
    /// <summary>
    /// Gestión Reporte de Cambio de Fase
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ConfigurarEvaluacionTrabajoController : ControllerBase
    {
        IConfigurarEvaluacionTrabajoService _configurarEvaluacionTrabajoService;
        public ConfigurarEvaluacionTrabajoController(IUnitOfWork unitOfWork)
        {
            _configurarEvaluacionTrabajoService = new ConfigurarEvaluacionTrabajoService(unitOfWork);
        }
        /// Metodo HTTP: POST.
        /// Autor: Gilmer Qm
        /// Fecha: 26/06/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción basica a la ta tabla y sus detalles
        /// </summary>   
        /// <param name="configurarEvaluacionTrabajoDTO"> parametros de la nueva Plantilla_PW y sus detalles </param>
        /// <returns> bool </returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult Insertar([FromBody] ConfigurarEvaluacionTrabajoDTO configurarEvaluacionTrabajoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var plantillaPaisFiltros = _configurarEvaluacionTrabajoService.Insertar(configurarEvaluacionTrabajoDTO, registroClaimToken.UserName);
                return Ok(plantillaPaisFiltros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Metodo HTTP: PUT.
        /// Autor: Gilmer Qm
        /// Fecha: 17/06/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción basica a la ta tabla y sus detalles
        /// </summary>   
        /// <param name="configurarEvaluacionTrabajoDTO"> parametros de la nueva Plantilla_PW y sus detalles </param>
        /// <returns> bool </returns>
        [Route("[action]")]
        [HttpPut]
        public IActionResult Actualizar([FromBody] ConfigurarEvaluacionTrabajoDTO configurarEvaluacionTrabajoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var plantillaPaisFiltros = _configurarEvaluacionTrabajoService.Actualizar(configurarEvaluacionTrabajoDTO, registroClaimToken.UserName);
                return Ok(plantillaPaisFiltros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Metodo HTTP: Delete.
        /// Autor: Gilmer Qm
        /// Fecha: 01/08/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica por el Primary Key
        /// </summary>   
        /// <param name="id"> (PK) </param>
        /// <returns> bool </returns>
        [Route("[action]/{id}")]
        [HttpDelete]
        public IActionResult Eliminar(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var plantillaPaisFiltros = _configurarEvaluacionTrabajoService.Eliminar(id, registroClaimToken.UserName);
                return Ok(plantillaPaisFiltros);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Metodo: HTTP: Get
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-01
        /// <summary>
        /// Obtiene pregunta evaluacion por configuracion
        /// </summary>
        /// <param name="idConfigurarEvaluacionTrabajo"></param>
        /// <returns>idConfigurarEvaluacionTrabajo, IdConfigurarEvaluacionTrabajo</returns>
        [Route("[action]/{idConfigurarEvaluacionTrabajo}")]
        [HttpGet]
        public IActionResult ObtenerPorConfiguracion(int idConfigurarEvaluacionTrabajo)
        {
            try
            {
                var evaluacion = _configurarEvaluacionTrabajoService.ObtenerPorConfiguracion(idConfigurarEvaluacionTrabajo);
                return Ok(evaluacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
