using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Servicios.Controllers.Tento
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class MaestroBsgTentoController : ControllerBase
    {
        private readonly IBsgTentoService _bsgTentoService;
        private readonly ITokenManager _tokenManager;

        public MaestroBsgTentoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _bsgTentoService = new BsgTentoService(unitOfWork);
            _tokenManager = tokenManager;
        }

        [HttpGet("[action]")]
        public IActionResult ObtenerAreasConRuta()
        {
            try { return Ok(_bsgTentoService.ObtenerAreasConRuta()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("[action]/{idAreaCapacitacion}")]
        public IActionResult ObtenerUnidadesPorArea(int idAreaCapacitacion)
        {
            try { return Ok(_bsgTentoService.ObtenerUnidadesPorArea(idAreaCapacitacion)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult InsertarUnidad([FromBody] BsgTentoUnidadInsertarDTO dto)
        {
            try { return Ok(_bsgTentoService.InsertarUnidad(dto, _tokenManager.UserName)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarUnidad([FromBody] BsgTentoUnidadActualizarDTO dto)
        {
            try { _bsgTentoService.ActualizarUnidad(dto, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarOrdenUnidades([FromBody] List<BsgTentoOrdenDTO> ordenList)
        {
            try { _bsgTentoService.ActualizarOrdenUnidades(ordenList, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{id}")]
        public IActionResult EliminarUnidad(int id)
        {
            try { _bsgTentoService.EliminarUnidad(id, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("[action]/{idBsgTentoUnidad}")]
        public IActionResult ObtenerPasosPorUnidad(int idBsgTentoUnidad)
        {
            try { return Ok(_bsgTentoService.ObtenerPasosPorUnidad(idBsgTentoUnidad)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult InsertarPaso([FromBody] BsgTentoPasoInsertarDTO dto)
        {
            try { return Ok(_bsgTentoService.InsertarPaso(dto, _tokenManager.UserName)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarPaso([FromBody] BsgTentoPasoActualizarDTO dto)
        {
            try { _bsgTentoService.ActualizarPaso(dto, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarOrdenPasos([FromBody] List<BsgTentoOrdenDTO> ordenList)
        {
            try { _bsgTentoService.ActualizarOrdenPasos(ordenList, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{id}")]
        public IActionResult EliminarPaso(int id)
        {
            try { _bsgTentoService.EliminarPaso(id, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("[action]/{idAreaCapacitacion}")]
        public IActionResult ObtenerComboPrograma(int idAreaCapacitacion)
        {
            try { return Ok(_bsgTentoService.ObtenerComboPrograma(idAreaCapacitacion)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
