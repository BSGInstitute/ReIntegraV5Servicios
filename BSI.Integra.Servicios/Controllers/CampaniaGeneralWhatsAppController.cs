using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.Sendingblue;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CampaniaGeneralWhatsAppController
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 2/06/2023
    /// <summary>
    /// Gestión de Campania General Whatsapp
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CampaniaGeneralWhatsAppController : Controller
    {
        private IUnitOfWork unitOfWork;
        public CampaniaGeneralWhatsAppController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerCampaniaGeneralDetalleWhatsApp(IdDTO id)
        {
            try
            {
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).ObtenerCampaniaGeneralDetalleWhatsApp(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarCampaniaGeneralWhatsApp(StringDTO nombre)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                //var Usuario = "achipanaa";
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).InsertarCampaniaGeneralWhatsApp(nombre, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerCampaniaGeneralWhatsApp(IdDTO id)
        {
            try
            {
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).ObtenerCampaniaGeneralWhatsApp(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarCampaniaGeneralWhatsApp(ActualizarCampaniaGeneralWhatsAppDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).ActualizarCampaniaGeneralWhatsApp(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCampaniaGeneralGrillaWhatsApp()
        {
            try
            {
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).ObtenerCampaniaGeneralGrillaWhatsApp());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult EliminarCampaniaGeneralWhatsApp(EliminarCampaniaGeneralWhatsAppDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).EliminarCampaniaGeneralWhatsApp(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarActivarMasivoPorCampania(ActualizarActivarMasivoPorCampaniaDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).ActualizarActivarMasivoPorCampania(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult EliminarCampaniaGeneralDetalleWhatsApp(EliminarCampaniaGeneralDetalleWhatsAppDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).EliminarCampaniaGeneralDetalleWhatsApp(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarCampaniaGeneralDetalleWhatsApp(InsertarCampaniaGeneralDetalleWhatsAppDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario= Usuario;
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).InsertarCampaniaGeneralDetalleWhatsApp(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarCampaniaGeneralDetalleExcelWhatsApp(InsertarCampaniaGeneralDetalleWhatsAppDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).InsertarCampaniaGeneralDetalleExcelWhatsApp(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarCamposCampaniaGeneralDetalleWhatsApp(ActualizarCamposCampaniaGeneralDetalleWhatsAppDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).ActualizarCamposCampaniaGeneralDetalleWhatsApp(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult ProcesarDataPorPrioridadSendinblue(ProcesarDataPorPrioridadSendinblueDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).ProcesarDataPorPrioridadSendinblue(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        

        [Route("[action]")]
        [HttpPost]
        public IActionResult ProcesarDataPorPrioridadExcel(ProcesarDataPorPrioridadExcelAlumnoDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).ProcesarDataPorPrioridadExcel(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerConfiguracionCampaniaGeneralDetalleWhatsApp(IdDTO json)
        {
            try
            {
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).ObtenerConfiguracionCampaniaGeneralDetalleWhatsApp(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerCampaniaGeneralDetalleResponsablePorPrioridad(IdDTO id)
        {
            try
            {
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).ObtenerCampaniaGeneralDetalleResponsablePorPrioridad(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [Route("[action]")]
        [HttpPost]
        public IActionResult EliminarCampaniaGeneralDetalleResponsableWhatsApp(EliminarCampaniaGeneralDetalleResponsableWhatsAppDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).EliminarCampaniaGeneralDetalleResponsableWhatsApp(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarCampaniaGeneralDetalleResponsableWhatsApp(InsertarCampaniaGeneralDetalleResponsableWhatsAppDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;

                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).InsertarCampaniaGeneralDetalleResponsableWhatsApp(json, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerComboCampaniaGeneralDetalleResponsableWhatsApp()
        {
            try
            {
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).ObtenerComboCampaniaGeneralDetalleResponsableWhatsApp());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerComboRespuestaWhatsAppp()
        {
            try
            {
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).ObtenerComboRespuestaWhatsAppp());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerComboCampaniasSendinBlue()
        {
            try
            {
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).ObtenerComboCampaniasSendinBlue());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerComboCentroCostoCampaniasSendinBlue()
        {
            try
            {
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).ObtenerComboCentroCostoCampaniasSendinBlue());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ReporteInteraccionCampaniaGeneralDetalle(IdDTO id)
        {
            try
            {
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).ReporteInteraccionCampaniaGeneralDetalle(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult SumaChatValidoWhatsApp(SumaValidadorChatDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).SumaChatValidoWhatsApp(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult RestaChatValidoWhatsApp(SumaValidadorChatDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).RestaChatValidoWhatsApp(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult SumaChatInValidoWhatsApp(SumaValidadorChatDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).SumaChatInValidoWhatsApp(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult RestaChatInValidoWhatsApp(SumaValidadorChatDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).RestaChatInValidoWhatsApp(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult SumaOportunidadWhatsApp(SumaOportunidadWhatsAootDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).SumaOportunidadWhatsApp(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult RestaOportunidadWhatsApp(SumaOportunidadWhatsAootDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).RestaOportunidadWhatsApp(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult WhatsAppPlantilla(WhatsAppPlantillaDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var Idusuario = _respuestaCorrecta.RegistroClaimToken.IdPersonal;
                json.IdPersonal = Idusuario;
                json.usuario = usuario;
                //var Usuario = "achipanaa";
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).EnvioMensajePorPlantilla(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult ListaWhatsAppPlantilla(WhatsAppPlantillaListaDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var Idusuario = _respuestaCorrecta.RegistroClaimToken.IdPersonal;
                json.IdPersonal = Idusuario;
                json.usuario = usuario;
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).EnvioListaMensajePorPlantilla(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult WhatsAppPlantillaFacebook(WhatsAppPlantillaDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var Idusuario = _respuestaCorrecta.RegistroClaimToken.IdPersonal;
                json.IdPersonal = 5962;//usuario WhatsappBSG
                json.usuario = usuario;
                //var Usuario = "achipanaa";
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).EnvioMensajePorPlantilla(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult WhatsAppMensajeTexto(WhatsAppMensajeTextoDTO json)
        {
            try
            {

                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var Idusuario = _respuestaCorrecta.RegistroClaimToken.IdPersonal;
                json.IdPersonal = Idusuario;
                json.usuario = usuario;
                //var Usuario = "achipanaa";

                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).EnvioMensajePorTexto(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    

        [Route("[action]")]
        [HttpPost]
        public IActionResult WhatsAppMensajeTextoFacebook(WhatsAppMensajeTextoFacebookDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var Idusuario = _respuestaCorrecta.RegistroClaimToken.IdPersonal;
                json.IdPersonal =5962;
                json.usuario = usuario;
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).EnvioMensajePorTextoFacebook(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult WhatsAppMensajeArchivo(WhatsAppMensajeArchivoFacebookDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;
                var Idusuario = _respuestaCorrecta.RegistroClaimToken.IdPersonal;
                var IdPersonal = 5962;//se mantiene logica de envio de mensaje 
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).EnvioMensajeArchivoFacebook(json, IdPersonal, usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerDiasPorPrioridadWhatsapp(IdDTO id)
        {
            try
            {
             
                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).ObtenerDiasPorPrioridadWhatsapp(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAlterno(IdDiasWhatsappDTO datos)
        {
            try
            {

                return Ok(new CampaniaGeneralWhatsAppService(unitOfWork).ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAlterno(datos));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}









