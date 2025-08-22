using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;


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
    public class GrupoComponenteEvaluacionController : ControllerBase
    {
        private ITokenManager _tokenManager;
        private IGrupoComponenteEvaluacionService _examenAsignadoEvaluadorService;
        public GrupoComponenteEvaluacionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _examenAsignadoEvaluadorService = new GrupoComponenteEvaluacionService(unitOfWork);
            _tokenManager = tokenManager;
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarAsignacionComponenteAEvaluacion([FromBody] AsignacionComponenteEvaluacionDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var res = _examenAsignadoEvaluadorService.ActualizarAsignacionComponenteAEvaluacion(Json, _tokenManager.UserName);
                string rpta = "ACTUALIZADO CORRECTAMENTE";
                return Ok(new { rpta });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarFactorGrupoComponente([FromBody] GrupoComponenteFactorDTO GrupoComponente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var res = _examenAsignadoEvaluadorService.ActualizarFactorGrupoComponente(GrupoComponente);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarGrupoComponente([FromBody] GrupoComponenteEvaluacionFormularioDTO Formulario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var rpta = _examenAsignadoEvaluadorService.ActualizarGrupoComponente(Formulario);
                return Ok(Formulario.GrupoComponenteEvaluacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult RegistrarGrupoComponente([FromBody] GrupoComponenteEvaluacionFormularioDTO CentilFormulario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var rpta = _examenAsignadoEvaluadorService.RegistrarGrupoComponente(CentilFormulario, _tokenManager.UserName);
                return Ok(rpta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPut]
        public ActionResult ActualizarCentilGrupoComponente([FromBody] ObjetoCentilCompuestoDTO CentilFormulario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var rpta = _examenAsignadoEvaluadorService.ActualizarCentilGrupoComponente(CentilFormulario);
                return Ok(rpta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
