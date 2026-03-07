using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using System;

using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion.WhatsAppMensajeEnviadoApiPlanificacionDTO;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// Controlador: WhatsAppMensajeEnviadoApiPlanificacionController
    /// Autor: Lolo Zaa
    /// Fecha: 06/03/2026
    /// <summary>
    /// Gestion de envio de mensajes WhatsApp para Planificacion.
    /// Patron basado en WhatsAppMensajeEnviadoApiComercialController
    /// pero usando IdProveedor y endpoint de Planificacion.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class WhatsAppMensajeEnviadoApiPlanificacionController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private ITokenManager _tokenManager;

        public WhatsAppMensajeEnviadoApiPlanificacionController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            this._unitOfWork = unitOfWork;
            this._tokenManager = tokenManager;
        }

        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPost]
        public IActionResult WhatsAppMensajeTexto(WhatsAppMensajeTextoPlaDTO json)
        {
            try
            {
                json.WaBody = json.WaBody.Replace("\t", "   ");
                json.WaBody = json.WaBody.Replace("\n", "   ");

                return Ok(new WhatsAppMensajeEnviadoApiPlanificacionService(_unitOfWork)
                    .EnvioMensajePorTexto(json, _tokenManager.UserName, (json.IdPersonal == null ? _tokenManager.IdPersonal : json.IdPersonal.Value)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPost]
        public IActionResult WhatsAppMensajePlantilla(WhatsAppMensajePlantillaPlaDTO json)
        {
            try
            {
                var token = _tokenManager;
                return Ok(new WhatsAppMensajeEnviadoApiPlanificacionService(_unitOfWork)
                    .EnvioMensajePorPlantilla(json, token.UserName, (json.IdPersonal == null ? token.IdPersonal : json.IdPersonal.Value)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPost]
        public IActionResult WhatsAppMensajeArchivo(WhatsAppMensajeArchivoPlaDTO json)
        {
            try
            {
                var token = _tokenManager;
                return Ok(new WhatsAppMensajeEnviadoApiPlanificacionService(_unitOfWork)
                    .EnvioMensajePorArchivo(json, token.UserName, (json.IdPersonal == null ? token.IdPersonal : json.IdPersonal.Value)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
