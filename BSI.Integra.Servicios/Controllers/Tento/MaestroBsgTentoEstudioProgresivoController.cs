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
    public class MaestroBsgTentoEstudioProgresivoController : ControllerBase
    {
        private readonly IBsgTentoService _bsgTentoService;
        private readonly ITokenManager _tokenManager;

        public MaestroBsgTentoEstudioProgresivoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _bsgTentoService = new BsgTentoService(unitOfWork);
            _tokenManager = tokenManager;
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Obtiene las áreas de capacitación que tienen ruta de estudio configurada en BSG Tento.
        /// </summary>
        /// <returns>Lista de áreas con ruta asociada.</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerAreasConRuta()
        {
            try { return Ok(_bsgTentoService.ObtenerAreasConRuta()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Obtiene las unidades del estudio progresivo correspondientes a un área de capacitación.
        /// </summary>
        /// <returns>Lista de unidades asociadas al área indicada.</returns>
        [HttpGet("[action]/{idAreaCapacitacion}")]
        public IActionResult ObtenerUnidadesPorArea(int idAreaCapacitacion)
        {
            try { return Ok(_bsgTentoService.ObtenerUnidadesPorArea(idAreaCapacitacion)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Registra una nueva unidad en el estudio progresivo de BSG Tento.
        /// </summary>
        /// <returns>Id del registro creado.</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult InsertarUnidad([FromBody] BsgTentoUnidadInsertarDTO dto)
        {
            try { return Ok(_bsgTentoService.InsertarUnidad(dto, _tokenManager.UserName)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Actualiza los datos de una unidad del estudio progresivo de BSG Tento.
        /// </summary>
        /// <returns>true si la operación fue exitosa.</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarUnidad([FromBody] BsgTentoUnidadActualizarDTO dto)
        {
            try { _bsgTentoService.ActualizarUnidad(dto, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Actualiza el orden de las unidades dentro del estudio progresivo de BSG Tento.
        /// </summary>
        /// <returns>true si la operación fue exitosa.</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarOrdenUnidades([FromBody] List<BsgTentoOrdenDTO> ordenList)
        {
            try { _bsgTentoService.ActualizarOrdenUnidades(ordenList, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Realiza la baja lógica de una unidad del estudio progresivo de BSG Tento.
        /// </summary>
        /// <returns>true si la operación fue exitosa.</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{id}")]
        public IActionResult EliminarUnidad(int id)
        {
            try { _bsgTentoService.EliminarUnidad(id, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Obtiene los pasos del estudio progresivo correspondientes a una unidad de BSG Tento.
        /// </summary>
        /// <returns>Lista de pasos asociados a la unidad indicada.</returns>
        [HttpGet("[action]/{idBsgTentoUnidad}")]
        public IActionResult ObtenerPasosPorUnidad(int idBsgTentoUnidad)
        {
            try { return Ok(_bsgTentoService.ObtenerPasosPorUnidad(idBsgTentoUnidad)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo paso dentro de una unidad del estudio progresivo de BSG Tento.
        /// </summary>
        /// <returns>Id del registro creado.</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult InsertarPaso([FromBody] BsgTentoPasoInsertarDTO dto)
        {
            try { return Ok(_bsgTentoService.InsertarPaso(dto, _tokenManager.UserName)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Actualiza los datos de un paso dentro de una unidad del estudio progresivo de BSG Tento.
        /// </summary>
        /// <returns>true si la operación fue exitosa.</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarPaso([FromBody] BsgTentoPasoActualizarDTO dto)
        {
            try { _bsgTentoService.ActualizarPaso(dto, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Actualiza el orden de los pasos dentro de una unidad del estudio progresivo de BSG Tento.
        /// </summary>
        /// <returns>true si la operación fue exitosa.</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarOrdenPasos([FromBody] List<BsgTentoOrdenDTO> ordenList)
        {
            try { _bsgTentoService.ActualizarOrdenPasos(ordenList, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Realiza la baja lógica de un paso del estudio progresivo de BSG Tento.
        /// </summary>
        /// <returns>true si la operación fue exitosa.</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{id}")]
        public IActionResult EliminarPaso(int id)
        {
            try { _bsgTentoService.EliminarPaso(id, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-15
        /// Version: 1.0
        /// <summary>
        /// Obtiene el listado de programas disponibles para selección según el área de capacitación.
        /// </summary>
        /// <returns>Lista para combo/selector de programas.</returns>
        [HttpGet("[action]/{idAreaCapacitacion}")]
        public IActionResult ObtenerComboPrograma(int idAreaCapacitacion)
        {
            try { return Ok(_bsgTentoService.ObtenerComboPrograma(idAreaCapacitacion)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
