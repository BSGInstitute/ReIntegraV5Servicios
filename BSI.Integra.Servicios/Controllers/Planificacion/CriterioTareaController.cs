using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Collections.Generic;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CriterioTareaController : ControllerBase
    {
        private readonly ICriterioTareaService _criterioTareaService;

        public CriterioTareaController(IUnitOfWork unitOfWork)
        {
            _criterioTareaService = new CriterioTareaService(unitOfWork);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ListarCriterio()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var resultado = _criterioTareaService.ListarCriterios();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]/{idCriterio}")]
        [HttpGet]
        public IActionResult ObtenerPorIdCriterio(int idCriterio)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var resultado = _criterioTareaService.ObtenerPorId(idCriterio);
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
        public IActionResult InsertarCriterio([FromBody] CriterioTareaDTO criterioDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _criterioTareaService.Insertar(criterioDTO, registroClaimToken.UserName);
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
        public IActionResult ActualizarCriterio([FromBody] CriterioTareaDTO criterioDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _criterioTareaService.Actualizar(criterioDTO, registroClaimToken.UserName);
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
        public IActionResult EliminarCriterio(int id)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _criterioTareaService.Eliminar(id, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]/{idCriterio}")]
        [HttpGet]
        public IActionResult ListarSubCriteriosPorCriterio(int idCriterio)
        {
            try
            {
                var resultado = _criterioTareaService.ListarSubCriteriosPorCriterio(idCriterio);
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
        public IActionResult AsignarSubCriterio([FromBody] AsignacionCriterioSubCriterioDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _criterioTareaService.AsignarSubCriterio(dto.IdCriterio, dto.IdSubCriterio, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]/{idCriterio}/{idSubCriterio}")]
        [HttpDelete]
        //[Authorize]
        public IActionResult DesasignarSubCriterio(int idCriterio, int idSubCriterio)
        {
            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                var resultado = _criterioTareaService.DesasignarSubCriterio(idCriterio, idSubCriterio, registroClaimToken.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
