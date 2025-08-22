using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Sendingblue;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingBlueCampaniasDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueContactosDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendingblueListaDTO;

namespace BSI.Integra.Servicios.Controllers.Marketing.Sendingblue
{
    /// Controlador: Lista
    /// Autor: Rodrigo Montesinos Paredes.
    /// Fecha: 11/22/20222
    /// <summary>
    /// Gestion de las listas de sendinblue
    /// </summary>
    [Authorize]
    [Route("api/marketing/sendinblue/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ListaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private readonly ISendingblueService sendingblue;
        private SendingblueRepositorioHelperService servicio;
        public ListaController(IUnitOfWork unitOfWork, ISendingblueService sendingblue)
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
        /// Realiza la obtencion de las listas desde sendinblue
        /// </summary>
        /// <param name="limit">Cantidad maxima de registros a obtener</param>
        /// <param name="offset">Principo de donde sera obtenidos los registros</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("offset/{limit}/{offset}")]
        [HttpGet]
        public IActionResult Listas(int limit ,long offset)
        {
            try
            {
                RespuestaGenerica respuesta = sendingblue.Listas(limit, offset);
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
        /// Realiza la creacion de una lista en sendinblue
        /// </summary>
        /// <param name="myListName">Objeto necesario para la creacion de una lista</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Crear")]
        [HttpPost]
        public IActionResult CrearLista([FromBody] CreacionDeListasSendingBlue myListName)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                RespuestaGenerica respuesta = sendingblue.CrearLista(myListName.id, myListName.Nombre);
                if(respuesta!= null)
                {
                    var lista = JsonConvert.DeserializeObject<CrearSendingblueListaDTO>(respuesta.SendingblueRespuesta);
                    var resp = servicio.AgregarLista(lista, _respuestaCorrecta.RegistroClaimToken.UserName);
                    if (resp.Response)
                    {
                        respuesta.error = resp;
                    }
                }
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
        /// Realiza la Carga de datos en lista
        /// </summary>
        /// <param name="myListName">Objeto necesario para la creacion de una lista</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Cargar/contactos/{idLista}")]
        [HttpPost]
        public IActionResult CrearLista(int idLista)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                RespuestaGenerica respuesta = sendingblue.UpdateCantidadDeContactosLista(idLista);
                if (respuesta != null)
                {
                    var lista = JsonConvert.DeserializeObject<CrearSendingblueListaDTO>(respuesta.SendingblueRespuesta);
                    var resp = servicio.ActualizaDataDeLista(lista, _respuestaCorrecta.RegistroClaimToken.UserName);
                    if (resp.Response)
                    {
                        respuesta.error = resp;
                    }
                }
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
        /// Realiza la actualizacion de una lista
        /// </summary>
        /// <param name="list">Actualizacion de una lista en sendinblue</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Actualizar")]
        [HttpPost]
        public async Task<IActionResult> UpdateLista([FromBody] ListUpdate list)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity); 
                RespuestaGenerica respuesta = await sendingblue.UpdateLista(list);
                if (!respuesta.error.Response)
                {
                    var resp = servicio.ActualizarLista(Convert.ToInt32(list.listId), list.name,Convert.ToInt32(list.folderId), _respuestaCorrecta.RegistroClaimToken.UserName);
                    if (resp.Response)
                    {
                        respuesta.error = resp;
                    }
                }
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 11/22/20222
        /// Versión: 1.0
        /// <summary>
        /// Realiza la eliminacion de una listas desde sendinblue
        /// </summary>
        /// <param name="idList">Identificador de lista</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Delete/{idList}")]
        [HttpPost]
        public async Task<IActionResult> DeleteLista(int idList)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                RespuestaGenerica respuesta = await sendingblue.DeleteLista(idList);
                if (!respuesta.error.Response)
                {
                    var resp = servicio.EliminarLista(idList, _respuestaCorrecta.RegistroClaimToken.UserName);
                    if (resp.Response)
                    {
                        respuesta.error = resp;
                    }
                }
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
        /// Realiza la obtencion los detalles de una lista por id de lista
        /// </summary>
        /// <param name="idlist">Identificador de lista</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Detalle/{idlist}")]
        [HttpGet]
        public IActionResult DetalleDeLista(long idlist)
        {
            try
            {
                RespuestaGenerica respuesta = sendingblue.DetalleDeLista(idlist);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 11/22/20222
        /// Versión: 1.0
        /// <summary>
        /// Realiza la eliminacion de una lista 
        /// </summary>
        /// <param name="CorreosEliminados">Correos que seran retirados de lista </param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Eliminar/Contactos/lista")]
        [HttpPost]
        public IActionResult EliminarContactosDeLista([FromBody] CrearContactosListaDto CorreosEliminados)
        {
            try
            { 
                RespuestaGenerica respuesta = sendingblue.EliminarContactosDeLista(CorreosEliminados);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
