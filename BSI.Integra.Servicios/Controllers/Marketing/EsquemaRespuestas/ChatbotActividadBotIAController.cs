using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.EsquemaRespuestas;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.EsquemaRespuestas;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Marketing.EsquemaRespuestas
{
    /// <summary>Controlador para la gestion de actividades Bot IA.</summary>
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsVista")]
    public class ChatbotActividadBotIAController : ControllerBase
    {
        private readonly IChatbotActividadBotIAService _chatbotActividadBotIAService;
        private readonly IEsquemaRespuestasService     _esquemaRespuestasService;

        public ChatbotActividadBotIAController(
            IChatbotActividadBotIAService chatbotActividadBotIAService,
            IEsquemaRespuestasService     esquemaRespuestasService)
        {
            _chatbotActividadBotIAService = chatbotActividadBotIAService;
            _esquemaRespuestasService     = esquemaRespuestasService;
        }

        /// <summary>Retorna el listado activo de actividades Bot IA con sus numeros asociados.</summary>
        [HttpGet]
        [Route("[action]")]
        public IActionResult ObtenerListado()
        {
            try
            {
                var listado = _chatbotActividadBotIAService.ObtenerListadoChatbotActividadBotIA();
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>Retorna el catalogo de numeros WhatsApp disponibles para asignacion.</summary>
        [HttpGet]
        [Route("[action]")]
        public IActionResult ObtenerListadoNumero()
        {
            try
            {
                var listado = _esquemaRespuestasService.ObtenerListadoNumero();
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>Inserta una nueva actividad Bot IA con sus numeros asociados.</summary>
        [HttpPost]
        [Route("[action]")]
        public IActionResult Insertar([FromBody] InsertarChatbotActividadBotIADTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var claimsIdentity     = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario            = _respuestaCorrecta.RegistroClaimToken.UserName;

                _chatbotActividadBotIAService.InsertarChatbotActividadBotIA(request, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>Actualiza una actividad Bot IA y sus numeros asociados.</summary>
        [HttpPost]
        [Route("[action]")]
        public IActionResult Actualizar([FromBody] ActualizarChatbotActividadBotIADTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var claimsIdentity     = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario            = _respuestaCorrecta.RegistroClaimToken.UserName;

                _chatbotActividadBotIAService.ActualizarChatbotActividadBotIA(request, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>Eliminacion logica de una actividad Bot IA y desactivacion de sus numeros.</summary>
        [HttpPost]
        [Route("[action]")]
        public IActionResult Eliminar([FromBody] EliminarChatbotActividadBotIADTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var claimsIdentity     = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario            = _respuestaCorrecta.RegistroClaimToken.UserName;

                _chatbotActividadBotIAService.EliminarChatbotActividadBotIA(request, usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
