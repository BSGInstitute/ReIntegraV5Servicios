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

namespace BSI.Integra.Servicios.Controllers.Tento
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class MaestroBsgTentoSuscripcionesController : ControllerBase
    {
        private readonly IBsgTentoSuscripcionService _bsgTentoSuscripcionService;
        private readonly ITokenManager _tokenManager;

        public MaestroBsgTentoSuscripcionesController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _bsgTentoSuscripcionService = new BsgTentoSuscripcionService(unitOfWork);
            _tokenManager = tokenManager;
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-16
        /// Version: 1.0
        /// <summary>
        /// Obtiene el catálogo de planes de suscripción configurados para BSG Tento
        /// </summary>
        /// <returns>Lista de planes activos con sus beneficios</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerPlanes()
        {
            try { return Ok(_bsgTentoSuscripcionService.ObtenerPlanes()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-16
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo plan de suscripción
        /// </summary>
        /// <returns>Id del plan creado</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPost("[action]")]
        public IActionResult InsertarPlan([FromBody] PlanSuscripcionInsertarDTO dto)
        {
            try { return Ok(_bsgTentoSuscripcionService.InsertarPlan(dto, _tokenManager.UserName)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-16
        /// Version: 1.0
        /// <summary>
        /// Actualiza los datos y beneficios de un plan de suscripción
        /// </summary>
        /// <returns>true si la operación fue exitosa</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult ActualizarPlan([FromBody] PlanSuscripcionActualizarDTO dto)
        {
            try { _bsgTentoSuscripcionService.ActualizarPlan(dto, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-16
        /// Version: 1.0
        /// <summary>
        /// Realiza la baja lógica de un plan de suscripción
        /// </summary>
        /// <returns>true si la operación fue exitosa</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{id}")]
        public IActionResult EliminarPlan(int id)
        {
            try { _bsgTentoSuscripcionService.EliminarPlan(id, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
