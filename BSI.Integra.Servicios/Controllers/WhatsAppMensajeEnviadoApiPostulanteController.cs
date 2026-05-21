using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: WhatsAppMensajeEnviadoApiPostulanteController
    /// Autor: Claude (sdd-apply Slice 2 — gp-whatsapp-endpoints)
    /// Fecha: 2026-05-13
    /// Version: 1.0
    /// <summary>
    /// API conversacional slim del chat WhatsApp para el asesor GP (postulantes).
    /// Lista pendientes y conversaciones del asesor logueado, consulta el historial
    /// crudo de un postulante y envia mensajes via proxy al WebHookWhatsApp.
    /// V5 NO emite SignalR — el front se conecta directo al hub externo (FR-7).
    /// </summary>
    [ApiController]
    [Authorize]
    [JwtExpirationValidation]
    [EnableCors("CorsVista")]
    [Route("api/[controller]")]
    public class WhatsAppMensajeEnviadoApiPostulanteController : ControllerBase
    {
        private readonly IWhatsAppMensajeEnviadoApiPostulanteService _service;
        private readonly ITokenManager                               _tokenManager;

        public WhatsAppMensajeEnviadoApiPostulanteController(
            IWhatsAppMensajeEnviadoApiPostulanteService service,
            ITokenManager                               tokenManager)
        {
            _service      = service;
            _tokenManager = tokenManager;
        }

        /// <summary>FR-1: postulantes asignados con ultimo mensaje recibido sin respuesta de ningun asesor.</summary>
        [HttpGet("MensajesPendientes")]
        public async Task<IActionResult> MensajesPendientes()
        {
            var pendientes = await _service.ObtenerPendientesAsync(_tokenManager.IdPersonal);
            return Ok(pendientes);
        }

        /// <summary>FR-2: ultimo mensaje por postulante asignado al asesor.</summary>
        [HttpGet("Conversaciones")]
        public async Task<IActionResult> Conversaciones()
        {
            var conversaciones = await _service.ObtenerConversacionesAsync(_tokenManager.IdPersonal);
            return Ok(conversaciones);
        }

        /// <summary>FR-3: hilo cronologico crudo del postulante. NotFoundException -> 404 via middleware.</summary>
        [HttpGet("Historial/{idPostulante:int}")]
        public async Task<IActionResult> Historial(int idPostulante, [FromQuery] int? idPais)
        {
            var historial = await _service.ObtenerHistorialAsync(idPostulante, idPais);
            return Ok(historial);
        }

        /// <summary>
        /// FR-4: proxy passthrough al WebHookWhatsApp.
        /// Excepciones del service (GatewayTimeout, BadGateway, BadRequest, Unauthorized)
        /// se propagan al GlobalExceptionHandlingMiddleware que las mapea a 504/502/400/401.
        /// </summary>
        [HttpPost("Enviar")]
        public async Task<IActionResult> Enviar([FromBody] EnviarMensajeWhatsAppPostulanteRequest request)
        {
            var resultado = await _service.EnviarMensajeAsync(request, _tokenManager.IdPersonal);
            return Ok(resultado);
        }

        /// <summary>
        /// FR-9: valida ventana Meta de 24h para texto libre. Espejo del endpoint ATC
        /// `WhatsAppMensajes/ValidarMesajesEnviadosEn24Horas/{numero}`.
        /// Retorna `bool` raw (sin DTO wrapper) para paridad con ATC:
        ///   true  = ventana CERRADA  => el front debe forzar plantilla Meta.
        ///   false = ventana ABIERTA  => el front puede mandar texto libre.
        /// </summary>
        [HttpGet("ValidarMesajesEnviadosEn24Horas/{numero}")]
        public async Task<IActionResult> ValidarMesajesEnviadosEn24Horas(string numero)
        {
            var resultado = await _service.ValidarVentana24HorasAsync(numero);
            return Ok(resultado);
        }
    }
}
