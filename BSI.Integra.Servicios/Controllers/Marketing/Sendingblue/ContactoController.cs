using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Mandrill;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingBlueCampaniasDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueContactosDTO;
using System.Net;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueSendersDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueCampaniasEnvioApiDTO;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Sendingblue;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;
using Microsoft.AspNetCore.Cors;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Sendingblue;
using static BBSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.TSendingblueContactosDTO;
using Microsoft.AspNetCore.Authorization;
using sib_api_v3_sdk.Model;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueRelacionAlmunoSBDTO;
using BSI.Integra.Servicios.Helpers;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers.Marketing.Sendingblue
{
    /// Controlador: Contacto
    /// Autor: Rodrigo Montesinos Paredes.
    /// Fecha: 11/22/20222
    /// <summary>
    /// Gestion de los contactos en sendinblue
    /// </summary>
    [Authorize]
    [Route("api/marketing/sendinblue/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ContactoController : ControllerBase
    {

        private IUnitOfWork unitOfWork;
        private readonly ISendingblueService sendingblue; 
        public ContactoController(IUnitOfWork unitOfWork, ISendingblueService sendingblue)
        {
            this.unitOfWork = unitOfWork;
            this.sendingblue = sendingblue;
        }
        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 11/22/20222
        /// Versión: 1.0
        /// <summary>
        /// Realiza la actualizar un contacto
        /// </summary>
        /// <param name="contacto">Datos necesarios para la actualizaicon de un contacto</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Actualizar")]
        [HttpPost]
        public async Task<IActionResult> ActualizarContactos([FromBody] SendingContactosDTO contacto)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                RespuestaGenerica respuesta = await sendingblue.ActualizarContactos(contacto);
                if (!respuesta.error.Response)
                {
                    var contactos = new SendingblueRepositorioHelperService(unitOfWork).ActualizarContactos(JsonConvert.DeserializeObject<CrearSendingblueContactos>(respuesta.SendingblueRespuesta), _respuestaCorrecta.RegistroClaimToken.UserName);
                    respuesta.error = contactos;
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
        /// Realiza la creacion de un nuevo contacto en sendinblue y la base de datos
        /// </summary>
        /// <param name="contacto">Datos necesarios para la creacion de un contacto</param>
        /// <returns>Una RespuestaGenerica </returns>
        [HttpPost]
        public IActionResult CrearContactos([FromBody] SendingContactosDTO contacto)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                RespuestaGenerica respuesta = sendingblue.CrearContactos(contacto);
                if (!respuesta.error.Response)
                {
                    PuenteControllerHelperCrearContacto datosObtenidos = JsonConvert.DeserializeObject<PuenteControllerHelperCrearContacto>(respuesta.SendingblueRespuesta);
                    var contactos = new SendingblueRepositorioHelperService(unitOfWork).AgregarContactosDeSendingblue(datosObtenidos.crearSendingblueContactos,_respuestaCorrecta.RegistroClaimToken.UserName);
                    var contactosRelacion = new SendingblueRepositorioHelperService(unitOfWork).AgregarUnaRelacionAlumnoSendingBlue(datosObtenidos.crearSendinblueRelacionAlmunoSB, _respuestaCorrecta.RegistroClaimToken.UserName);
                    if (contactos.Response)
                    {
                        respuesta.error.Response = true;
                        respuesta.error.Detalle.Descripcion.Concat("\n" + contactos.Detalle.Descripcion );
                        respuesta.error.Detalle.Mensaje.Concat("\n" + contactos.Detalle.Mensaje );
                    }
                    if (contactosRelacion.Response)
                    {
                        respuesta.error.Response = true;
                        respuesta.error.Detalle.Descripcion.Concat("\n" + contactosRelacion.Detalle.Descripcion);
                        respuesta.error.Detalle.Mensaje.Concat("\n" + contactosRelacion.Detalle.Mensaje);
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
        /// Realiza la eliminacion de un contacto de sendinblue por email
        /// </summary>
        /// <param name="email">Email de contacto a eliminar</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("CrearMasivo")]
        [HttpPost]
        public IActionResult CrearContactos([FromBody] List<SendingContactosDTO> contacto)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                List<CrearSendinblueRelacionAlmunoSB> contacsRelacion = new List<CrearSendinblueRelacionAlmunoSB>();
                List<CrearSendingblueContactos> contacts = new List<CrearSendingblueContactos>();                
                RespuestaGenerica respuesta = new();
                foreach (var i in contacto)
                {
                    respuesta = sendingblue.CrearContactos(i);
                    var deserealizacion = JsonConvert.DeserializeObject<PuenteControllerHelperCrearContacto>(respuesta.SendingblueRespuesta);
                    contacts.Add(deserealizacion.crearSendingblueContactos);
                    contacsRelacion.Add(deserealizacion.crearSendinblueRelacionAlmunoSB);
                }


                var contactos = new SendingblueRepositorioHelperService(unitOfWork).AgregarContactosDeSendingblueArray(contacts, _respuestaCorrecta.RegistroClaimToken.UserName);
                var contactosRelacion = new SendingblueRepositorioHelperService(unitOfWork).AgregarRelacionAlumnoSendingBlue(contacsRelacion, _respuestaCorrecta.RegistroClaimToken.UserName);
                if (contactos.Response)
                {
                    respuesta.error.Response = true;
                    respuesta.error.Detalle.Descripcion.Concat("\n" + contactos.Detalle.Descripcion);
                    respuesta.error.Detalle.Mensaje.Concat("\n" + contactos.Detalle.Mensaje);
                }
                if (contactosRelacion.Response)
                {
                    respuesta.error.Response = true;
                    respuesta.error.Detalle.Descripcion.Concat("\n" + contactosRelacion.Detalle.Descripcion);
                    respuesta.error.Detalle.Mensaje.Concat("\n" + contactosRelacion.Detalle.Mensaje);
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
        /// Realiza la eliminacion de un contacto de sendinblue por email
        /// </summary>
        /// <param name="email">Email de contacto a eliminar</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Eliminar/poremail")]
        [HttpPost]
        public async Task<IActionResult> EliminarContactos([FromBody] eliminarContacto email)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                RespuestaGenerica respuesta = await sendingblue.EliminarContactos(email.email.ToString());
                if (!respuesta.error.Response)
                {
                    var contactos = new SendingblueRepositorioHelperService(unitOfWork).EliminarContacto(email.email.ToString(), _respuestaCorrecta.RegistroClaimToken.UserName);
                    respuesta.error = contactos;
                }
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
        /// Realiza la obtencion de los contactos desde sendinblue
        /// </summary>
        /// <param name="limit">Cantidad maxima de registros a obtener</param>
        /// <param name="offset">Principo de donde sera obtenidos los registros</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("limit/{limit}/{offset}")]
        [HttpGet]
        public IActionResult ConseguirContactos(long limit, long offset)
        {
            try
            {

                RespuestaGenerica respuesta = sendingblue.ConseguirContactos(limit,offset);
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
        /// Realiza la obtencion de los contactos desde sendinblue
        /// </summary>
        /// <param name="limit">Cantidad maxima de registros a obtener</param>
        /// <param name="offset">Principo de donde sera obtenidos los registros</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("listar/{limit}/{offset}")]
        [HttpGet]

        public IActionResult ListarContactos(int limit,long offset)
        {
            try
            {
                RespuestaGenerica respuesta = sendingblue.ListarContactos(limit, offset);
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
        /// Realiza la creacion de un contacto nuevo en sendinblue y en la base de datos
        /// </summary>
        /// <param name="nuevoCorreo">DTO de contacto</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Agregar/Lista")]
        [HttpPost]
        public ActionResult AgregarContactosALista([FromBody] CrearContactosListaDto nuevoCorreo)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                RespuestaGenerica respuesta = sendingblue.AgregarContactosALista(nuevoCorreo);
                if (!respuesta.error.Response)
                {
                    new SendingblueRepositorioHelperService(unitOfWork).AgregarRelacionContactosLista(nuevoCorreo, _respuestaCorrecta.RegistroClaimToken.UserName);
                    
                }
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 11/22/20222
        /// Versión: 1.0
        /// <summary>
        /// Realiza la obtencion de los contactos por lista desde sendinblue
        /// </summary>
        /// <param name="limit">Cantidad maxima de registros a obtener</param>
        /// <param name="offset">Principo de donde sera obtenidos los registros</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Obtener/por/Lista/{idDelist}/{limit}/{offset}")]
        [HttpGet]
        public IActionResult ObtenerContactosPorLista(long idDelist, int limit,long offset)
        {
            try
            {
                RespuestaGenerica respuesta = sendingblue.ObtenerContactosPorLista(idDelist, limit, offset);
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}
