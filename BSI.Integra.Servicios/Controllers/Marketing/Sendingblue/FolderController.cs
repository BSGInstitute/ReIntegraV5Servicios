using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Sendingblue;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using MailBee.ImapMail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueCarpetaDTO;

namespace BSI.Integra.Servicios.Controllers.Marketing.Sendingblue
{
    /// Controlador: Folder
    /// Autor: Rodrigo Montesinos Paredes.
    /// Fecha: 11/22/20222
    /// <summary>
    /// Gestion de las listas de sendinblue
    /// </summary>
    [Authorize]
    [Route("api/marketing/sendinblue/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class FolderController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private readonly ISendingblueService sendingblue;
        private SendingblueRepositorioHelperService servicio;
        public FolderController(IUnitOfWork unitOfWork, ISendingblueService sendingblue)
        {
            this.unitOfWork = unitOfWork;
            this.sendingblue = sendingblue;
            this.servicio = new SendingblueRepositorioHelperService(unitOfWork);
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 11/22/20222
        /// Versión: 1.0
        /// <summary>
        /// Realiza la obtencion de las folders desde sendinblue
        /// </summary>
        /// <param name="limit">Cantidad maxima de registros a obtener</param>
        /// <param name="offset">Principo de donde sera obtenidos los registros</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("limit/{limit}/{offset}")]
        [HttpGet]
        public IActionResult ListarFolders(int limit,long offset)
        {
            try
            {
                RespuestaGenerica respuesta = sendingblue.ListarFolders(limit, offset);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 11/22/20222
        /// Versión: 1.0
        /// <summary>
        /// Realiza la creacion de un folder en sendinblue
        /// </summary>
        /// <param name="myFolderName">Nombre usado para la creacion del folder</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Crear")]
        [HttpPost]
        public IActionResult CrearFolder([FromBody] CrearFolderSendinblue myFolderName)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                RespuestaGenerica respuesta = sendingblue.CrearFolder(myFolderName.myFolderName);
                var res= JsonConvert.DeserializeObject<CrearSendinblueCarpeta>(respuesta.SendingblueRespuesta);
                var CarpetaRes = servicio.AgregarCarpeta(res, myFolderName.myFolderName, _respuestaCorrecta.RegistroClaimToken.UserName);
                respuesta.error = CarpetaRes;
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 11/22/20222
        /// Versión: 1.0
        /// <summary>
        /// Realiza la actualizacion de un folder en sendinblue
        /// </summary>
        /// <param name="idFolder">ientificador del folder</param>
        /// <param name="name">Nombre usado para la creacion del folder</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Actualizar/idFolder/{idFolder}")]
        [HttpPost]
        public async Task<IActionResult> ActualizarFolder(long idFolder,[FromBody] CrearFolderSendinblue name)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                RespuestaGenerica respuesta = await sendingblue.ActualizarFolder(idFolder,name.myFolderName);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 11/22/20222
        /// Versión: 1.0
        /// <summary>
        /// Realiza la eliminacion de un folder en sendinblue
        /// </summary>
        /// <param name="folderId">identificador de folder</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("idFolder/{folderId}")]
        [HttpPost]
        public async Task<IActionResult> EliminarFolder(long folderId)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                RespuestaGenerica respuesta = await sendingblue.EliminarFolder(folderId);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 11/22/20222
        /// Versión: 1.0
        /// <summary>
        /// obtiene las listas que pertenecen al folder por el id de folder de sendinblue
        /// </summary>
        /// <param name="idfolder">identificador de folder</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Obtener/Listas/por/folder/{idfolder}")]
        [HttpGet]
        public IActionResult ObtenerLasListasPorFolder(int idfolder)
        {
            try
            {
                RespuestaGenerica respuesta = servicio.ObtenerFolderMasListas(idfolder);
                return Ok(respuesta);
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }
    }
}
