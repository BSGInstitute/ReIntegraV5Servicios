using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Sendingblue;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueSendersDTO;

namespace BSI.Integra.Servicios.Controllers.Marketing.Sendingblue
{
    /// Controlador: Sender
    /// Autor: Rodrigo Montesinos Paredes.
    /// Fecha: 11/22/20222
    /// <summary>
    /// Gestion de los senders de sendinblue
    /// </summary>
    [Authorize]
    [Route("api/marketing/sendinblue/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class SenderController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private readonly ISendingblueService sendingblue;
        public SenderController(IUnitOfWork unitOfWork, ISendingblueService sendingblue)
        {
            this.unitOfWork = unitOfWork;
            this.sendingblue = sendingblue;
        }

        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 11/22/20222
        /// Versión: 1.0
        /// <summary>
        /// Realiza la obtencion de los senders desde sendinblue
        /// </summary>
        /// <returns>Una RespuestaGenerica </returns>
        [HttpGet("getSenders")]
        public IActionResult ObtenerSenders()
        {
            try
            {
                SendingblueObtenerSenders respuesta = sendingblue.ObtenerSenders();
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 11/22/20222
        /// Versión: 1.0
        /// <summary>
        /// Realiza la creacion de un sender en sendinblue
        /// </summary>
        /// <param name="sendersDTO">Objeto necesario para la creacion de un sender en sendinblue</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Crear")]
        [HttpPost]
        public IActionResult AgregarSender([FromBody] SengindblueSenders sendersDTO)
        {
            try
            {
                SendingblueSendersRespuesta respuesta = sendingblue.AgregarSender(sendersDTO);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 11/22/20222
        /// Versión: 1.0
        /// <summary>
        /// Realiza la actualizacion de un sender
        /// </summary>
        /// <param name="sendersDTO">objeto para la actualizacion de los datos de un sender</param>
        /// <param name="idSender">Identificador unico el sender</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Actualizar")]
        [HttpPost]
        public async Task<IActionResult> ActualizarSender([FromBody] SengindblueSenders sendersDTO, int idSender)
        {
            try
            {
                RespuestaGenerica respuesta = await sendingblue.ActualizarSender(sendersDTO, idSender);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 11/22/20222
        /// Versión: 1.0
        /// <summary>
        /// Realiza la eliminacion de un sender en sendinblue
        /// </summary>
        /// <param name="idSender">Identificador de sender</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Eliminar/{idSender}")]
        [HttpPost]
        public async Task<IActionResult> EliminarSender(int idSender)
        {
            try
            {
                RespuestaGenerica respuesta = await sendingblue.EliminarSender(idSender);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
