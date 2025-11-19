using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Messenger;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Messenger;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Marketing.Messenger
{
    // Controlador: MessengerFacebookChatController
    // Autor: Humberto Oscata
    // Fecha: 11/11/2025
    // <summary>
    // Gestión de chats de Messenger (paginas Facebook BSG) para Marketing
    // </summary>
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsVista")]
    public class MessengerFacebookChatController : ControllerBase
    {
        IUnitOfWork _unitOfWork;
        IMessengerFacebookChatService _messengerFacebookChatService;

        public MessengerFacebookChatController(IUnitOfWork unitOfWork, IMessengerFacebookChatService messengerFacebookChatService)
        {
            this._unitOfWork = unitOfWork;
            this._messengerFacebookChatService = messengerFacebookChatService;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult ObtenerGrillaChats([FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin, [FromQuery] string tipo = "todas")
        {
            try
            {
                List<ResumenMessengerFacebookChatDTO> result = _messengerFacebookChatService.ObtenerGrillaChats(fechaInicio, fechaFin, tipo);

                return Ok(result ?? new List<ResumenMessengerFacebookChatDTO>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al obtener los chats");
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult ObtenerHistorialChatPorPSID([FromBody] ObtenerHistorialChatPorPSIDRequestDTO request)
        {
            try
            {
                List<ChatMessengerFacebookDTO> result = _messengerFacebookChatService.ObtenerHistorialChatPorPSID(request);

                if(result == null || result.Count == 0)
                    return NotFound("No se encontró historial de chat para el identificador");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al obtener el historial del contacto");
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> EnviarMensajeTexto([FromBody] EnviarMensajeTextoRequest request)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                int idPersonal = _respuestaCorrecta.RegistroClaimToken.IdPersonal;
                string usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                request.IdPersonal = idPersonal;
                request.UsuarioCreacion = usuario;

                EnviarMensajeResponse result = await _messengerFacebookChatService.EnviarMensajeTexto(request);

                if (result.Success)
                    return Ok(new { enviado = true, mensaje = "Mensaje enviado correctamente." });
                else
                    return BadRequest(new { enviado = false, mensaje = result.Message ?? "No se pudo enviar el mensaje por una razón desconocida." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { enviado = false, mensaje = "Ocurrió un error interno del servidor." });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult ObtenerDatosGeneralesAlumnosPorPSID([FromBody] ObtenerDatosGeneralesAlumnosPorPSIDRequestDTO request)
        {
            try
            {
                List<ObtenerDatosGeneralesAlumnosPorPSIDResponseDTO> result = _messengerFacebookChatService.ObtenerDatosGeneralesAlumnosPorPSID(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al obtener alumnos registrados del contacto");
            }
        }

    }
}
