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
    public class MaestroBsgTentoPowerUpsController : ControllerBase
    {
        private readonly IBsgTentoPowerUpService _bsgTentoPowerUpService;
        private readonly ITokenManager _tokenManager;

        public MaestroBsgTentoPowerUpsController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _bsgTentoPowerUpService = new BsgTentoPowerUpService(unitOfWork);
            _tokenManager = tokenManager;
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-16
        /// Version: 1.0
        /// <summary>
        /// Obtiene el catálogo de power-ups configurados para BSG Tento
        /// </summary>
        /// <returns>Lista de power-ups activos</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerPowerUps()
        {
            try { return Ok(_bsgTentoPowerUpService.ObtenerPowerUps()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-16
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo power-up en el catálogo
        /// </summary>
        /// <returns>Id del power-up creado</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult InsertarPowerUp([FromBody] BsgTentoPowerUpInsertarDTO dto)
        {
            try { return Ok(_bsgTentoPowerUpService.InsertarPowerUp(dto, _tokenManager.UserName)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-16
        /// Version: 1.0
        /// <summary>
        /// Actualiza los datos de un power-up existente
        /// </summary>
        /// <returns>true si la operación fue exitosa</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarPowerUp([FromBody] BsgTentoPowerUpActualizarDTO dto)
        {
            try { _bsgTentoPowerUpService.ActualizarPowerUp(dto, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-16
        /// Version: 1.0
        /// <summary>
        /// Actualiza el orden de visualización de los power-ups
        /// </summary>
        /// <returns>true si la operación fue exitosa</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarOrdenPowerUps([FromBody] List<BsgTentoOrdenDTO> ordenList)
        {
            try { _bsgTentoPowerUpService.ActualizarOrdenPowerUps(ordenList, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-16
        /// Version: 1.0
        /// <summary>
        /// Realiza la baja lógica de un power-up
        /// </summary>
        /// <returns>true si la operación fue exitosa</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{id}")]
        public IActionResult EliminarPowerUp(int id)
        {
            try { _bsgTentoPowerUpService.EliminarPowerUp(id, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
