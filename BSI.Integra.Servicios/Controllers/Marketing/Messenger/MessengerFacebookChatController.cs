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

        /// Autor: Humberto Oscata
        /// Fecha: 11/11/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene la grilla de mensajes de messenger entrantes o salientes respecto de un rango de fechas
        /// </summary>
        /// <param name="fechaInicio">fecha inicio rango</param>
        /// <param name="fechaFin">fecha fin rango</param>
        /// <param name="tipo">Tipo de mensajes (entrante, saliente)</param>
        /// <returns>Lista de ultimos mensajes messenger por PSID</returns>
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

        /// Autor: Humberto Oscata
        /// Fecha: 12/11/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene el historial de chats para un identificador de ambito de pagina
        /// </summary>
        /// <param name="request">Objeto que contiene el identificadoAmbitoPagina</param>
        /// <returns>Lista de mensajes messenger</returns>
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

        /// Autor: Humberto Oscata
        /// Fecha: 19/11/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos generales de alumnos registrados por PSID
        /// </summary>
        /// <param name="request">Objeto que contiene el identificadoAmbitoPagina</param>
        /// <returns>Listado de alumnos y sus detalles por PSID</returns>
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

        /// Autor: Humberto Oscata
        /// Fecha: 14/11/2025
        /// Version: 1.0
        /// <summary>
        /// Envia un mensaje de texto
        /// </summary>
        /// <param name="request">Objeto con los detalles del mensaje</param>
        /// <returns>Objeto con cofirmacion de envio y posible mensaje error</returns>
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
    }
}
