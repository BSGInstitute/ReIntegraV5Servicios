using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Messenger;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Messenger;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult ObtenerGrillaChats([FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin, [FromQuery] string tipo = "all")
        {
            try
            {
                List<ResumenMessengerFacebookChatDTO> result = _messengerFacebookChatService.ObtenerGrillaChats(fechaInicio, fechaFin, tipo);

                return Ok(result ?? new List<ResumenMessengerFacebookChatDTO>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno al obtener los chats");
            }
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult ObtenerHistorialChatPorUsuario()
        {
            // Lógica para obtener los chats de Messenger
            return Ok("Funcionalidad para obtener chats de Messenger aún no implementada.");
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult ObtenerHistorialChatPorPSID()
        {
            // Lógica para obtener los chats de Messenger
            return Ok("Funcionalidad para obtener chats de Messenger aún no implementada.");
        }


    }
}
