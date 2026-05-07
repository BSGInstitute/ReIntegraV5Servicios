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
    /// Controlador: FacebookConfiguracionController
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Gestión de páginas de Facebook para BSG Institute.
    /// Almacena identificador de página, nombre, token de acceso, total de opiniones y valoración.
    /// Flujo: Controller → FacebookConfiguracionService → FacebookConfiguracionRepository → EF Core.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FacebookConfiguracionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenManager _tokenManager;

        public FacebookConfiguracionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
        }

        /// <summary>Instancia el servicio de dominio con el UnitOfWork actual.</summary>
        private IFacebookConfiguracionService CrearServicio()
            => new FacebookConfiguracionService(_unitOfWork);

        /// Tipo Función: GET
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las configuraciones activas de páginas de Facebook.
        /// </summary>
        /// <returns>Lista de FacebookConfiguracion activas.</returns>
        [HttpGet("ObtenerTodos")]
        public IActionResult ObtenerTodos()
        {
            try { return Ok(CrearServicio().ObtenerTodos()); }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// Tipo Función: POST
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta una nueva página de Facebook.</summary>
        /// <param name="entidad">Entidad FacebookConfiguracion a insertar.</param>
        /// <returns>Entidad insertada.</returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] FacebookConfiguracion entidad)
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
        /// <summary>Actualiza una página de Facebook.</summary>
        /// <param name="entidad">Entidad FacebookConfiguracion con los datos actualizados.</param>
        /// <returns>Entidad actualizada.</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] FacebookConfiguracion entidad)
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
        /// <summary>Elimina lógicamente una página de Facebook por su Id.</summary>
        /// <param name="id">Id de la página a eliminar.</param>
        /// <returns>true si se eliminó correctamente.</returns>
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
