using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace BSI.Integra.Servicios.Controllers.GestionPersonas
{
    /// Controlador: CategoriaPreguntaController
    /// Autor: Villanueva Torres Marco Jose
    /// Fecha: 16/04/2024
    /// <summary>
    /// Criterio Evaluacion Proceso
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    [Authorize]
    [JwtExpirationValidation]
    public class ExamenController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IExamenService _examenAsignadoEvaluadorService;
        public ExamenController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _examenAsignadoEvaluadorService = new ExamenService(unitOfWork);
            _tokenManager = tokenManager;
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarFactorComponente([FromBody] FactorComponenteDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var respuesta = _examenAsignadoEvaluadorService.ActualizarFactorComponente(Json);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("ObtenerComponentesPorEvaluacion")]
        public IActionResult ObtenerComponentesPorEvaluacion(int idEvaluacion)
        {
            try
            {
                var listaExamen = _examenAsignadoEvaluadorService.ObtenerComponentesPorEvaluacion(idEvaluacion);
                return Ok(listaExamen); 
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //////////ObtenerExamenes
        [HttpGet("[action]")]
        public IActionResult ObtenerEvaluacion()
        {
            try
            {
                var listaExamen = _examenAsignadoEvaluadorService.ObtenerEvaluacion();
                return Ok(listaExamen);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
