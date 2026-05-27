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
    public class MaestroBsgTentoSocialController : ControllerBase
    {
        private readonly IBsgTentoSocialService _bsgTentoSocialService;
        private readonly ITokenManager _tokenManager;

        public MaestroBsgTentoSocialController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _bsgTentoSocialService = new BsgTentoSocialService(unitOfWork);
            _tokenManager = tokenManager;
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-16
        /// Version: 1.0
        /// <summary>
        /// Obtiene publicaciones del feed social para moderación, filtradas por rango de fecha de modificación y, opcionalmente, por visibilidad
        /// </summary>
        /// <param name="fechaInicio">Fecha inicial del rango de modificación (obligatoria)</param>
        /// <param name="fechaFin">Fecha final del rango de modificación (obligatoria)</param>
        /// <param name="visible">Filtro opcional por visibilidad de la publicación</param>
        /// <returns>Lista de publicaciones con datos de moderación</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerPublicaciones(DateTime fechaInicio, DateTime fechaFin, bool? visible = null)
        {
            try { return Ok(_bsgTentoSocialService.ObtenerPublicaciones(visible, fechaInicio, fechaFin)); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-16
        /// Version: 1.0
        /// <summary>
        /// Actualiza la visibilidad de una publicación (ocultar o mostrar)
        /// </summary>
        /// <returns>true si la operación fue exitosa</returns>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]/{id}/{visible}")]
        [HttpPut]
        public IActionResult ActualizarVisibilidadPublicacion(int id, bool visible)
        {
            try { _bsgTentoSocialService.ActualizarVisibilidadPublicacion(id, visible, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-16
        /// Version: 1.0
        /// <summary>
        /// Realiza la baja lógica de una publicación
        /// </summary>
        /// <returns>true si la operación fue exitosa</returns>
        [Authorize]
        [JwtExpirationValidation]
        [HttpDelete("[action]/{id}")]
        public IActionResult EliminarPublicacion(int id)
        {
            try { _bsgTentoSocialService.EliminarPublicacion(id, _tokenManager.UserName); return Ok(true); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 2026-05-23
        /// Version: 1.0
        /// <summary>
        /// Obtiene el catálogo de tipos de reacción disponibles para las publicaciones del feed social
        /// </summary>
        /// <returns>Lista de tipos de reacción ordenados</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerTiposReaccion()
        {
            try { return Ok(_bsgTentoSocialService.ObtenerTiposReaccion()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
