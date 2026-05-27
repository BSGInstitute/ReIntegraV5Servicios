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
    public class MaestroBsgTentoLogrosController : ControllerBase
    {
        private readonly IBsgTentoLogrosService _bsgTentoLogrosService;
        private readonly ITokenManager _tokenManager;

        public MaestroBsgTentoLogrosController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _bsgTentoLogrosService = new BsgTentoLogrosService(unitOfWork);
            _tokenManager = tokenManager;
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Obtiene el catálogo de tipos de condición disponibles para logros y misiones
        /// </summary>
        /// <returns>Lista de tipos de condición activos</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerTiposCondicion()
        {
            try { return Ok(_bsgTentoLogrosService.ObtenerTiposCondicion()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de logros activos, filtrable por tipo (1=Logro, 2=Racha)
        /// </summary>
        /// <returns>Lista de logros con datos relacionados</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerLogros(int? tipoLogro = null)
        {
            try { return Ok(_bsgTentoLogrosService.ObtenerLogros(tipoLogro)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo logro o racha en el catálogo de BSG Tento
        /// </summary>
        /// <returns>Id del logro creado</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult InsertarLogro([FromBody] BsgTentoLogroInsertarDTO dto)
        {
            try { return Ok(_bsgTentoLogrosService.InsertarLogro(dto, _tokenManager.UserName)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Actualiza los datos de un logro o racha existente
        /// </summary>
        /// <returns>true si la operación fue exitosa</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarLogro([FromBody] BsgTentoLogroActualizarDTO dto)
        {
            try { _bsgTentoLogrosService.ActualizarLogro(dto, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Actualiza el orden de visualización de múltiples logros en una sola operación
        /// </summary>
        /// <returns>true si la operación fue exitosa</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarOrdenLogros([FromBody] List<BsgTentoOrdenDTO> ordenList)
        {
            try { _bsgTentoLogrosService.ActualizarOrdenLogros(ordenList, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Realiza la baja lógica de un logro (Estado = 0)
        /// </summary>
        /// <returns>true si la operación fue exitosa</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{id}")]
        public IActionResult EliminarLogro(int id)
        {
            try { _bsgTentoLogrosService.EliminarLogro(id, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Obtiene el catálogo de tipos de misión (Diaria, Semanal, Eterna)
        /// </summary>
        /// <returns>Lista de tipos de misión activos</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerTiposMision()
        {
            try { return Ok(_bsgTentoLogrosService.ObtenerTiposMision()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de misiones activas, filtrable por tipo (1=Diaria, 2=Semanal, 3=Eterna)
        /// </summary>
        /// <returns>Lista de misiones con datos relacionados</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerMisiones(int? tipoMision = null)
        {
            try { return Ok(_bsgTentoLogrosService.ObtenerMisiones(tipoMision)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Registra una nueva misión en el catálogo de BSG Tento
        /// </summary>
        /// <returns>Id de la misión creada</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult InsertarMision([FromBody] BsgTentoMisionInsertarDTO dto)
        {
            try { return Ok(_bsgTentoLogrosService.InsertarMision(dto, _tokenManager.UserName)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Actualiza los datos de una misión existente
        /// </summary>
        /// <returns>true si la operación fue exitosa</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarMision([FromBody] BsgTentoMisionActualizarDTO dto)
        {
            try { _bsgTentoLogrosService.ActualizarMision(dto, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Realiza la baja lógica de una misión (Estado = 0)
        /// </summary>
        /// <returns>true si la operación fue exitosa</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{id}")]
        public IActionResult EliminarMision(int id)
        {
            try { _bsgTentoLogrosService.EliminarMision(id, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
