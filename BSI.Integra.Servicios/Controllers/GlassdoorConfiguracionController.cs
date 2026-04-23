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
    /// Controlador: GlassdoorConfiguracionController
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Gestión de cuentas de empleador en Glassdoor para BSG Institute.
    /// Almacena rating general, total de evaluaciones, URL del perfil y EmployerId.
    /// API pública de Glassdoor descontinuada en 2023 — captura manual.
    /// Flujo: Controller → GlassdoorConfiguracionService → GlassdoorConfiguracionRepository → EF Core.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class GlassdoorConfiguracionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenManager _tokenManager;

        public GlassdoorConfiguracionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
        }

        /// <summary>Instancia el servicio de dominio con el UnitOfWork actual.</summary>
        private IGlassdoorConfiguracionService CrearServicio()
            => new GlassdoorConfiguracionService(_unitOfWork);

        /// Tipo Función: GET
        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la configuración activa de la cuenta de empleador en Glassdoor.
        /// Retorna rating general (Valoracion), total de evaluaciones, URL del perfil e IdentificadorCuenta.
        /// </summary>
        /// <returns>GlassdoorConfiguracion activa o null.</returns>
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
        /// <summary>Inserta una nueva cuenta de empleador en Glassdoor.</summary>
        /// <param name="entidad">Entidad GlassdoorConfiguracion a insertar.</param>
        /// <returns>Entidad insertada.</returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] GlassdoorConfiguracion entidad)
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
        /// <summary>Actualiza la cuenta de empleador (rating, total evaluaciones, URL, EmployerId).</summary>
        /// <param name="entidad">Entidad GlassdoorConfiguracion con los datos actualizados.</param>
        /// <returns>Entidad actualizada.</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] GlassdoorConfiguracion entidad)
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
        /// <summary>Elimina lógicamente una cuenta de empleador por su Id.</summary>
        /// <param name="id">Id de la cuenta a eliminar.</param>
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
