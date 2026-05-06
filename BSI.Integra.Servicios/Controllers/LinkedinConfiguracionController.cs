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
    /// Controlador: LinkedinConfiguracionController
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Gestión de páginas de LinkedIn para BSG Institute.
    /// CRUD de la configuración (nombre, enlace, total opiniones).
    /// Flujo: Controller → LinkedinConfiguracionService → LinkedinConfiguracionRepository → EF Core.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class LinkedinConfiguracionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenManager _tokenManager;

        public LinkedinConfiguracionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
        }

        /// <summary>Instancia el servicio de dominio con el UnitOfWork actual.</summary>
        private ILinkedinConfiguracionService CrearServicio()
            => new LinkedinConfiguracionService(_unitOfWork);

        /// Tipo Función: GET
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Obtiene la configuración activa de la página de LinkedIn.</summary>
        /// <returns>LinkedinConfiguracion</returns>
        [HttpGet("Obtener")]
        public IActionResult Obtener()
        {
            try { return Ok(CrearServicio().Obtener()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta una nueva página de LinkedIn.</summary>
        /// <param name="entidad">Entidad LinkedinConfiguracion a insertar.</param>
        /// <returns>Entidad insertada.</returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] LinkedinConfiguracion entidad)
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
        /// <summary>Actualiza la página de LinkedIn.</summary>
        /// <param name="entidad">Entidad LinkedinConfiguracion con los datos actualizados.</param>
        /// <returns>Entidad actualizada.</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] LinkedinConfiguracion entidad)
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
        /// <summary>Elimina lógicamente una página de LinkedIn por su Id.</summary>
        /// <param name="id">Id de la página a eliminar.</param>
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
