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
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.IntegracionConIntegraDB.UpdateCampania;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingBlueCampaniasDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueCampaniasEnvioApiDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueSendersDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueCamapaniaDTO;

namespace BSI.Integra.Servicios.Controllers.Marketing.Sendingblue
{
    /// Controlador: Contacto
    /// Autor: Rodrigo Montesinos Paredes.
    /// Fecha: 11/22/20222
    /// <summary>
    /// Gestion de los contactos en sendinblue
    /// </summary>
    //[Authorize]
    [Route("api/marketing/sendinblue/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CampaniaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private readonly ISendingblueService sendingblue;
        private SendingblueRepositorioHelperService servicio;
        public CampaniaController(IUnitOfWork unitOfWork, ISendingblueService sendingblue)
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
        /// obtiene un listado de campanias
        /// </summary>
        /// <param name="offset">limite inferior para la busqeuda de datos</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("PorTipos")]
        [HttpPost]
        public IActionResult SendinblueCampania(GetEmailCampaignsDTO emailCampaigns)
        {
            try
            {
                RespuestaGenerica respuesta = sendingblue.SendinblueCampania(emailCampaigns);
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
        /// Realiza la creacion de una campania
        /// </summary>
        /// <param name="campania">Datos necesarios para la creacion de la campania</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Crear")]
        [HttpPost]
        public IActionResult CrearCampaignEmail([FromBody] CrearCampaniaSendinblue campania)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                RespuestaGenerica respuesta = sendingblue.CrearCampaignEmail(campania);
                if (!respuesta.error.Response)
                {
                    var retorno = servicio.AgregarCampanias(JsonConvert.DeserializeObject<CrearSendinblueCamapaniaDTO>(respuesta.SendingblueRespuesta), _respuestaCorrecta.RegistroClaimToken.UserName);
                    if (retorno.Response)
                    {
                        respuesta.error = retorno;
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
        /// Realiza la creacion de campania A/B
        /// </summary>
        /// <param name="campania">Datos necesarios par la creacion de una campania a/b </param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Crear/AbTest")]
        [HttpPost]
        public IActionResult CrearCampaignEmailAB([FromBody] CrearCampaniaSendinblueABTest campania)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                RespuestaGenerica respuesta = sendingblue.AgregarCampaniaABTest(campania);
                if (!respuesta.error.Response)
                {
                    var retorno = servicio.AgregarCampanias(JsonConvert.DeserializeObject<CrearSendinblueCamapaniaDTO>(respuesta.SendingblueRespuesta), _respuestaCorrecta.RegistroClaimToken.UserName);
                    if (retorno.Response)
                    {
                        respuesta.error = retorno;
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
        /// Realiza la actualizar una campania
        /// </summary>
        /// <param name="sendersDTO">Datos necesarios par al actualizacion de senders</param>
        /// <param name="idSender">Id del sender</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("update/IdCampain/{idSender}")]
        [HttpPost]
        public IActionResult ActualizarCamapania([FromBody] UpdateCampaniaDTO sendersDTO, int idSender)
        {
            try
            {
                RespuestaGenerica respuesta = sendingblue.ActualizarCampania(sendersDTO, idSender);
                return Ok(respuesta);
            }catch(Exception ex)
            {
                return BadRequest();
            }
        }
        /// Tipo Función: POST
        /// Autor: Rodrigo Montesinos Paredes.
        /// Fecha: 11/22/20222
        /// Versión: 1.0
        /// <summary>
        /// Realiza el envio de campania por id de ampania
        /// </summary>
        /// <param name="campaignId">Identificador de campanias</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("mandar/{campaignId}")]
        [HttpPost]
        public IActionResult MandarCampaniaPorId(long campaignId)
        {
            try
            {
                RespuestaGenerica respuesta = sendingblue.MandarCampaniaPorId(campaignId);
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
        /// Realiza la actualizar una campania
        /// </summary>
        /// <param name="idCampania">Identificador de campania</param>
        /// <param name="estado">Estado ed campania</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("Actualizar/estado/{idCampania}/{estado}")]
        [HttpPost]
        public async Task<IActionResult> ActualizarCampania(int idCampania, string estado)
        {
            try
            {
                RespuestaGenerica respuesta = await sendingblue.ActualziarCampania(idCampania,estado);
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
        /// obtiene los datos de una campania
        /// </summary>
        /// <param name="idCampana">Identificador de campania</param>
        /// <returns>Una RespuestaGenerica </returns>
        [Route("ObtenerCampania/{idCampana}")]
        [HttpGet]
        public IActionResult ObtenerCampaniaPorId(int idCampana)
        {
            try
            {
                RespuestaGenerica respuesta = sendingblue.ObtenerCampaniaPorId(idCampana);
                return Ok(respuesta);
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }


        [Route("CrearCampaignEmailHtmlContent")]
        [HttpPost]
        public IActionResult CrearCampaignEmailHtmlContent([FromBody] CrearCampaignEmailHtmlContentDTO campania)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                RespuestaGenerica respuesta = sendingblue.CrearCampaignEmailHtmlContent(campania);
                if (!respuesta.error.Response)
                {
                    //var retorno = servicio.AgregarCampanias(JsonConvert.DeserializeObject<CrearSendinblueCamapaniaDTO>(respuesta.SendingblueRespuesta), _respuestaCorrecta.RegistroClaimToken.UserName);
                    var retorno = servicio.AgregarCampanias(JsonConvert.DeserializeObject<CrearSendinblueCamapaniaDTO>(respuesta.SendingblueRespuesta), Usuario);
                    if (retorno.Response)
                    {
                        respuesta.error = retorno;
                    }
                }
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        [Route("CrearCampaignEmailABHtmlContent")]
        [HttpPost]
        public IActionResult CrearCampaignEmailABHtmlContent([FromBody] CrearCampaniaSendinblueABTestHtmlContent campania)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                RespuestaGenerica respuesta = sendingblue.AgregarCampaniaABTestHtmlContent(campania);
                if (!respuesta.error.Response)
                {
                    var retorno = servicio.AgregarCampanias(JsonConvert.DeserializeObject<CrearSendinblueCamapaniaDTO>(respuesta.SendingblueRespuesta), _respuestaCorrecta.RegistroClaimToken.UserName);
                    if (retorno.Response)
                    {
                        respuesta.error = retorno;
                    }
                }
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
