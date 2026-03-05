using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class SubCriterioTareaController : ControllerBase
    {
        private readonly ISubCriterioTareaService _subCriterioTareaService;

        public SubCriterioTareaController(IUnitOfWork unitOfWork)
        {
            _subCriterioTareaService = new SubCriterioTareaService(unitOfWork);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ListarSubCriterio()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var resultado = _subCriterioTareaService.ListarSubCriterios();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]/{idSubCriterio}")]
        [HttpGet]
        public IActionResult ObtenerPorIdSubCriterio(int idSubCriterio)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var resultado = _subCriterioTareaService.ObtenerPorId(idSubCriterio);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        //[Authorize]
        public IActionResult InsertarSubCriterio([FromBody] SubCriterioTareaDTO subCriterioDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _subCriterioTareaService.Insertar(subCriterioDTO, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPut]
        //[Authorize]
        public IActionResult ActualizarSubCriterio([FromBody] SubCriterioTareaDTO subCriterioDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _subCriterioTareaService.Actualizar(subCriterioDTO, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        //[Authorize]
        public IActionResult EliminarSubCriterio(int id)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _subCriterioTareaService.Eliminar(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
