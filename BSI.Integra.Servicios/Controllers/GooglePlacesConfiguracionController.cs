using BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: GooglePlacesConfiguracionController
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Gestión de sedes de Google Places para BSG Institute.
    /// CRUD de la configuración (nombre, identificador, valoración, total reseñas).
    /// Flujo: Controller → GooglePlacesConfiguracionService → GooglePlacesConfiguracionRepository → EF Core.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class GooglePlacesConfiguracionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenManager _tokenManager;

        public GooglePlacesConfiguracionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
        }

        /// <summary>Instancia el servicio de dominio con el UnitOfWork actual.</summary>
        private IGooglePlacesConfiguracionService CrearServicio()
            => new GooglePlacesConfiguracionService(_unitOfWork);

        /// Tipo Función: GET
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Obtiene todas las configuraciones activas de sedes de Google Places.</summary>
        /// <returns>List de GooglePlacesConfiguracion</returns>
        [HttpGet("ObtenerTodos")]
        public IActionResult ObtenerTodos()
        {
            try { return Ok(CrearServicio().ObtenerTodos()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: GET
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Obtiene las sedes para el combo de selección del frontend.</summary>
        /// <returns>List de GooglePlacesConfiguracionComboDTO</returns>
        [HttpGet("[action]")]
        public IActionResult ObtenerCombo()
        {
            try { return Ok(CrearServicio().ObtenerCombo()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta una nueva sede de Google Places.</summary>
        /// <param name="entidad">Entidad GooglePlacesConfiguracion a insertar.</param>
        /// <returns>Entidad insertada.</returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] GooglePlacesConfiguracion entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                try
                {
                    entidad.UsuarioCreacion ??= _respuestaCorrecta.RegistroClaimToken.UserName;
                    entidad.UsuarioModificacion ??= _respuestaCorrecta.RegistroClaimToken.UserName;
                    return Ok(CrearServicio().Add(entidad));
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        /// Tipo Función: PUT
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Actualiza una sede de Google Places.</summary>
        /// <param name="entidad">Entidad GooglePlacesConfiguracion con los datos actualizados.</param>
        /// <returns>Entidad actualizada.</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] GooglePlacesConfiguracion entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                try
                {
                    entidad.UsuarioModificacion ??= _respuestaCorrecta.RegistroClaimToken.UserName;
                    return Ok(CrearServicio().Update(entidad));
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        /// Tipo Función: DELETE
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Elimina lógicamente una sede de Google Places por su Id.</summary>
        /// <param name="id">Id de la sede a eliminar.</param>
        /// <returns>bool</returns>
        [HttpDelete("Eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    return Ok(CrearServicio().Delete(id, _respuestaCorrecta.RegistroClaimToken.UserName));
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }
    }
}
