using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers
{
    /// <summary>
    /// Controller para los catálogos globales de esquemas WhatsApp (Mensajes Exactos, Fases, Perfiles)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CatalogosEsquemaWhatsAppController : ControllerBase
    {
        private readonly IMensajeExactoService _mensajeExactoService;
        private readonly IFaseService _faseService;
        private readonly IPerfilService _perfilService;

        public CatalogosEsquemaWhatsAppController(
            IMensajeExactoService mensajeExactoService,
            IFaseService faseService,
            IPerfilService perfilService)
        {
            _mensajeExactoService = mensajeExactoService;
            _faseService = faseService;
            _perfilService = perfilService;
        }

        #region Mensajes Exactos

        [HttpPost("MensajeExacto/Insertar")]
        public async Task<IActionResult> InsertarMensajeExacto([FromBody] MensajeExactoRequestDTO entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                try
                {
                    var resultado = await _mensajeExactoService.InsertarAsync(entidad, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
                {
                return Unauthorized();
            }
        }

        [HttpPut("MensajeExacto/Actualizar")]
        public async Task<IActionResult> ActualizarMensajeExacto([FromBody] MensajeExactoRequestDTO entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                try
                {
                    var resultado = await _mensajeExactoService.ActualizarAsync(entidad, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete("MensajeExacto/Eliminar/{id}")]
        public async Task<IActionResult> EliminarMensajeExacto(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                try
                {
                    var resultado = await _mensajeExactoService.EliminarAsync(id, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("MensajeExacto/Listar")]
        public async Task<IActionResult> ListarMensajesExactos()
        {
            try
            {
                var resultado = await _mensajeExactoService.ListarAsync();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Fases

        [HttpPost("Fase/Insertar")]
        public async Task<IActionResult> InsertarFase([FromBody] FaseRequestDTO entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                try
                {
                    var resultado = await _faseService.InsertarAsync(entidad, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut("Fase/Actualizar")]
        public async Task<IActionResult> ActualizarFase([FromBody] FaseRequestDTO entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                try
                {
                    var resultado = await _faseService.ActualizarAsync(entidad, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete("Fase/Eliminar/{id}")]
        public async Task<IActionResult> EliminarFase(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                try
                {
                    var resultado = await _faseService.EliminarAsync(id, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("Fase/Listar")]
        public async Task<IActionResult> ListarFases()
        {
            try
            {
                var resultado = await _faseService.ListarAsync();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Perfiles

        [HttpPost("Perfil/Insertar")]
        public async Task<IActionResult> InsertarPerfil([FromBody] PerfilRequestDTO entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                try
                {
                    var resultado = await _perfilService.InsertarAsync(entidad, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPut("Perfil/Actualizar")]
        public async Task<IActionResult> ActualizarPerfil([FromBody] PerfilRequestDTO entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                try
                {
                    var resultado = await _perfilService.ActualizarAsync(entidad, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete("Perfil/Eliminar/{id}")]
        public async Task<IActionResult> EliminarPerfil(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                try
                {
                    var resultado = await _perfilService.EliminarAsync(id, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("Perfil/Listar")]
        public async Task<IActionResult> ListarPerfiles()
        {
            try
            {
                var resultado = await _perfilService.ListarAsync();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}
