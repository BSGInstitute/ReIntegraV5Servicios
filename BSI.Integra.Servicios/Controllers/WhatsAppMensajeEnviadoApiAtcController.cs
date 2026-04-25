using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;

using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsAppMensajeEnviadoApiAtcDTO;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: WhatsAppMensajeEnviadoApiAtcController
    /// Autor: Alexis Arroyo
    /// Fecha: 15/04/2026
    /// Version: 1.0
    /// <summary>
    /// Intermediario backend para envio de mensajes WhatsApp ATC.
    /// Recibe el mismo payload que el frontend enviaba directamente al webhook,
    /// aplica la logica del chatbot ATC y delega el envio real al webhook externo.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class WhatsAppMensajeEnviadoApiAtcController : ControllerBase
    {
        private readonly IWhatsAppMensajeEnviadoApiAtcService _service;
        private readonly ITokenManager                        _tokenManager;

        public WhatsAppMensajeEnviadoApiAtcController(
            IWhatsAppMensajeEnviadoApiAtcService service,
            ITokenManager                        tokenManager)
        {
            _service      = service;
            _tokenManager = tokenManager;
        }

        /// TipoFuncion: POST
        /// <summary>
        /// Valida la logica del chatbot ATC y envia el mensaje WhatsApp al destinatario.
        /// Acepta el mismo payload que el webhook: texto, plantilla y archivo.
        /// </summary>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> EnviarMensajeValidacion([FromBody] WhatsAppEnviarMensajeDTO json)
        {
            try
            {
                var resultado = await _service.EnviarMensajeValidacion(json, _tokenManager.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// TipoFuncion: POST
        /// <summary>
        /// Finaliza una conversacion activa de WhatsApp ATC cambiando su estado a CERRADA_ASESOR.
        /// Busca el hilo por IdAlumno (primario) o WaTo (fallback).
        /// </summary>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> FinalizarConversacion([FromBody] FinalizarConversacionDTO json)
        {
            try
            {
                var resultado = await _service.FinalizarConversacion(json, _tokenManager.UserName);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
