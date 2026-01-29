using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class EsquemaWhatsAppController : ControllerBase
    {
        private readonly IEsquemaWhatsAppAsignacionService _esquemaService;
        private readonly IEsquemaLecturaMensajeService _lecturaMensajeService;
        private readonly IEsquemaInterpretarInformacionService _interpretarService;
        private readonly IEsquemaRespuestaService _respuestaService;
        private readonly IEsquemaActividadService _actividadService;

        public EsquemaWhatsAppController(
            IEsquemaWhatsAppAsignacionService esquemaService,
            IEsquemaLecturaMensajeService lecturaMensajeService,
            IEsquemaInterpretarInformacionService interpretarService,
            IEsquemaRespuestaService respuestaService,
            IEsquemaActividadService actividadService)
        {
            _esquemaService = esquemaService;
            _lecturaMensajeService = lecturaMensajeService;
            _interpretarService = interpretarService;
            _respuestaService = respuestaService;
            _actividadService = actividadService;
        }

        #region Esquema Principal

        [HttpPost("Insertar")]
        public async Task<IActionResult> InsertarEsquema([FromBody] EsquemaWhatsAppAsignacionRequestDTO entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                try
                {
                    var resultado = await _esquemaService.InsertarAsync(entidad, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else { return Unauthorized(); }
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> ActualizarEsquema([FromBody] EsquemaWhatsAppAsignacionRequestDTO entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                try
                {
                    var resultado = await _esquemaService.ActualizarAsync(entidad, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> EliminarEsquema(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                try
                {
                    var resultado = await _esquemaService.EliminarAsync(id, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        [HttpGet("ObtenerPorId/{id}")]
        public async Task<IActionResult> ObtenerEsquemaPorId(int id)
        {
            try
            {
                var resultado = await _esquemaService.ObtenerPorIdAsync(id);
                return Ok(resultado);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> ListarEsquemas()
        {
            try
            {
                var resultado = await _esquemaService.ListarAsync();
                return Ok(resultado);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        #endregion

        #region Lectura de Mensajes

        [HttpPost("LecturaMensaje/Insertar")]
        public async Task<IActionResult> InsertarLecturaMensaje([FromBody] EsquemaLecturaMensajeRequestDTO entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                try
                {
                    var resultado = await _lecturaMensajeService.InsertarAsync(entidad, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        [HttpPut("LecturaMensaje/Actualizar")]
        public async Task<IActionResult> ActualizarLecturaMensaje([FromBody] EsquemaLecturaMensajeRequestDTO entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                try
                {
                    var resultado = await _lecturaMensajeService.ActualizarAsync(entidad, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        [HttpDelete("LecturaMensaje/Eliminar/{id}")]
        public async Task<IActionResult> EliminarLecturaMensaje(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                try
                {
                    var resultado = await _lecturaMensajeService.EliminarAsync(id, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        [HttpGet("LecturaMensaje/ObtenerPorId/{id}")]
        public async Task<IActionResult> ObtenerLecturaMensajePorId(int id)
        {
            try
            {
                var resultado = await _lecturaMensajeService.ObtenerPorIdAsync(id);
                return Ok(resultado);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("LecturaMensaje/ListarPorEsquema/{idEsquema}")]
        public async Task<IActionResult> ListarLecturasMensajePorEsquema(int idEsquema)
        {
            try
            {
                var resultado = await _lecturaMensajeService.ListarPorEsquemaAsync(idEsquema);
                return Ok(resultado);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        #endregion

        #region Interpretar Información

        [HttpPost("InterpretarInformacion/Insertar")]
        public async Task<IActionResult> InsertarInterpretarInformacion([FromBody] EsquemaInterpretarInformacionRequestDTO entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                try
                {
                    var resultado = await _interpretarService.InsertarAsync(entidad, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        [HttpPut("InterpretarInformacion/Actualizar")]
        public async Task<IActionResult> ActualizarInterpretarInformacion([FromBody] EsquemaInterpretarInformacionRequestDTO entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                try
                {
                    var resultado = await _interpretarService.ActualizarAsync(entidad, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        [HttpDelete("InterpretarInformacion/Eliminar/{id}")]
        public async Task<IActionResult> EliminarInterpretarInformacion(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                try
                {
                    var resultado = await _interpretarService.EliminarAsync(id, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        [HttpGet("InterpretarInformacion/ObtenerPorId/{id}")]
        public async Task<IActionResult> ObtenerInterpretarInformacionPorId(int id)
        {
            try
            {
                var resultado = await _interpretarService.ObtenerPorIdAsync(id);
                return Ok(resultado);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("InterpretarInformacion/ListarPorEsquema/{idEsquema}")]
        public async Task<IActionResult> ListarInterpretarInformacionPorEsquema(int idEsquema)
        {
            try
            {
                var resultado = await _interpretarService.ListarPorEsquemaAsync(idEsquema);
                return Ok(resultado);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        #endregion

        #region Respuestas

        [HttpPost("Respuesta/Insertar")]
        public async Task<IActionResult> InsertarRespuesta([FromBody] EsquemaRespuestaRequestDTO entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                try
                {
                    var resultado = await _respuestaService.InsertarAsync(entidad, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        [HttpPut("Respuesta/Actualizar")]
        public async Task<IActionResult> ActualizarRespuesta([FromBody] EsquemaRespuestaActualizarDTO entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                try
                {
                    var resultado = await _respuestaService.ActualizarAsync(entidad, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        [HttpGet("Respuesta/ListarPorEsquema/{idEsquema}")]
        public async Task<IActionResult> ListarRespuestasPorEsquema(int idEsquema)
        {
            try
            {
                var resultado = await _respuestaService.ListarPorEsquemaAsync(idEsquema);
                return Ok(resultado);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        #endregion

        #region Actividad

        [HttpPost("Actividad/Insertar")]
        public async Task<IActionResult> InsertarActividad([FromBody] EsquemaActividadRequestDTO entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                try
                {
                    var resultado = await _actividadService.InsertarAsync(entidad, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        [HttpPut("Actividad/Actualizar")]
        public async Task<IActionResult> ActualizarActividad([FromBody] EsquemaActividadRequestDTO entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                try
                {
                    var resultado = await _actividadService.ActualizarAsync(entidad, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        [HttpPut("Actividad/ActualizarEstado")]
        public async Task<IActionResult> ActualizarEstadoActividad([FromBody] EsquemaActividadEstadoDTO entidad)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                try
                {
                    var resultado = await _actividadService.ActualizarEstadoAsync(entidad, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        [HttpDelete("Actividad/Eliminar/{id}")]
        public async Task<IActionResult> EliminarActividad(int id)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            if (respuestaCorrecta.TokenValida)
            {
                try
                {
                    var resultado = await _actividadService.EliminarAsync(id, respuestaCorrecta.RegistroClaimToken.UserName);
                    return Ok(resultado);
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else { return Unauthorized(); }
        }

        [HttpGet("Actividad/Listar")]
        public async Task<IActionResult> ListarActividades()
        {
            try
            {
                var resultado = await _actividadService.ListarAsync();
                return Ok(resultado);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        #endregion
    }
}
