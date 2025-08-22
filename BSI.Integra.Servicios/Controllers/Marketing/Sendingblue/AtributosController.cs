using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Sendingblue;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using sib_api_v3_sdk.Model;
using System.Security.Claims;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueAtributoDTO;

namespace BSI.Integra.Servicios.Controllers.Marketing.Sendingblue
{
    // Controlador: Contacto
    /// Autor: Rodrigo Montesinos Paredes.
    /// Fecha: 11/22/20222
    /// <summary>
    /// Gestion de los contactos en sendinblue
    /// </summary>
    [Authorize]
    [Route("api/marketing/sendinblue/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AtributosController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private readonly ISendingblueService sendingblue;
        private SendingblueRepositorioHelperService servicio;
        public AtributosController(IUnitOfWork unitOfWork, ISendingblueService sendingblue)
        {
            this.unitOfWork = unitOfWork;
            this.sendingblue = sendingblue;
            this.servicio = new SendingblueRepositorioHelperService(unitOfWork);
        }
        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 11/22/20222
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los atributos desde sendinblue
        /// </summary>
        /// <returns>Una RespuestaGenerica </returns>
        [HttpGet]
        public IActionResult ObtenerTodosLosatributos()
        {
            try
            {
                RespuestaGenerica respuesta = sendingblue.ObtenerTodosLosatributos();
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
        /// Realiza la insercion de un atriibuto
        /// </summary>
        /// <param name="categoria">Categoria de atributo</param>
        /// <param name="nombre">Nombre de atributo</param>
        /// <param name="tipo">tipo de atributo</param>
        /// <param name="enumerations">Listado de atributos enumeados</param>
        /// <returns>Una RespuestaGenerica </returns>
        [HttpPost("{categoria}/{nombre}/{tipo}")]
        public IActionResult AgregarAtributo(string categoria, string nombre,[FromBody] List<CreateAttributeEnumeration> enumerations, string tipo)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                RespuestaGenerica respuesta = sendingblue.AgregarAtributos(categoria,nombre,enumerations,tipo);
                CrearSendinblueAtributo ObjetoRespuesta =JsonConvert.DeserializeObject<CrearSendinblueAtributo>(respuesta.SendingblueRespuesta);
                var res = servicio.AgregarAtributosDeSensingBlue(ObjetoRespuesta,_respuestaCorrecta.RegistroClaimToken.UserName);
                respuesta.error = res;
                return Ok(respuesta);
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }
    }
}
