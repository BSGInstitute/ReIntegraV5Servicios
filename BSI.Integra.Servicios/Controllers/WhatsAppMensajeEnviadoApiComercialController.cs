using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsAppMensajeEnviadoApiComercialDTO;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: WhatsAppMensajeEnviadoApiComercialController
    /// Autor: Christian Quispe Mamani
    /// Fecha: 2/06/2023
    /// <summary>
    /// Gestión de Campania General Whatsapp
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class WhatsAppMensajeEnviadoApiComercialController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private ITokenManager _tokenManager;
        public WhatsAppMensajeEnviadoApiComercialController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            this._unitOfWork = unitOfWork;
            this._tokenManager = tokenManager;
        }
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]")]
        [HttpPost]
        public IActionResult WhatsAppMensajeTexto(WhatsAppMensajeTextoComDTO json)
        {
            try
            {
                // json.WaBody = json.WaBody.Replace("á", "a");
                // json.WaBody = json.WaBody.Replace("é", "e");
                // json.WaBody = json.WaBody.Replace("í", "i");
                // json.WaBody = json.WaBody.Replace("ó", "o");
                // json.WaBody = json.WaBody.Replace("ú", "u");

                //json.WaBody = json.WaBody.Replace("Á", "A");
                //json.WaBody = json.WaBody.Replace("É", "E");
                //json.WaBody = json.WaBody.Replace("Í", "I");
                //json.WaBody = json.WaBody.Replace("Ó", "O");
                //json.WaBody = json.WaBody.Replace("Ú", "U");

                // //Elimina las Ñ
                // json.WaBody = json.WaBody.Replace("ñ", "n");
                // json.WaBody = json.WaBody.Replace("Ñ", "N");

                //Elimina la  \t
                json.WaBody = json.WaBody.Replace("\t", "   ");
                json.WaBody = json.WaBody.Replace("\n", "   ");

                return Ok(new WhatsAppMensajeEnviadoApiComercialService(_unitOfWork).EnvioMensajePorTexto(json, _tokenManager.UserName, (json.IdPersonal == null ? _tokenManager.IdPersonal : json.IdPersonal.Value)));
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
        public async Task<IActionResult> AsistenteComercialMensajeTexto(AsistenteComercialMensajeTextoComDTO json)
        {
            try
            {
                //return Ok();
                WhatsAppMensajeEnviadoApiComercialService servicio = new WhatsAppMensajeEnviadoApiComercialService(_unitOfWork);
                var resultado = await servicio.EnvioMensajeAsistenteComercialPorTexto(json, _tokenManager.UserName);
                return Ok(resultado);
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
        public IActionResult WhatsAppMensajePlantilla(WhatsAppMensajePlantillaComDTO json)
        {
            try
            {
                var token = _tokenManager;
                return Ok(new WhatsAppMensajeEnviadoApiComercialService(_unitOfWork).EnvioMensajePorPlantilla(json, token.UserName, (json.IdPersonal == null ? token.IdPersonal : json.IdPersonal.Value)));
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
        public IActionResult WhatsAppMensajeArchivo(WhatsAppMensajeArchivoComDTO json)
        {
            try
            {
                var token = _tokenManager;
                return Ok(new WhatsAppMensajeEnviadoApiComercialService(_unitOfWork).EnvioMensajePorArchivo(json, token.UserName, (json.IdPersonal == null ? token.IdPersonal : json.IdPersonal.Value)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
