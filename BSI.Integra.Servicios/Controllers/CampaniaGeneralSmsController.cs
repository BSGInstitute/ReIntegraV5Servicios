using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CampaniaGeneralSmsController
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 2/06/2023
    /// <summary>
    /// Gestión de Campania General Sms
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class CampaniaGeneralSmsController : Controller
    {
        private IUnitOfWork unitOfWork;
        public CampaniaGeneralSmsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerCampaniaGeneralDetalleSms(IdDTO id)
        {
            try
            {
                return Ok(new CampaniaGeneralSmsService(unitOfWork).ObtenerCampaniaGeneralDetalleSms(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarCampaniaGeneralSms(StringDTO nombre)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                //var Usuario = "achipanaa";
                return Ok(new CampaniaGeneralSmsService(unitOfWork).InsertarCampaniaGeneralSms(nombre, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerCampaniaGeneralSms(IdDTO id)
        {
            try
            {
                return Ok(new CampaniaGeneralSmsService(unitOfWork).ObtenerCampaniaGeneralSms(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarCampaniaGeneralSms(ActualizarCampaniaGeneralSmsDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralSmsService(unitOfWork).ActualizarCampaniaGeneralSms(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCampaniaGeneralGrillaSms()
        {
            try
            {
                return Ok(new CampaniaGeneralSmsService(unitOfWork).ObtenerCampaniaGeneralGrillaSms());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult EliminarCampaniaGeneralSms(EliminarCampaniaGeneralSmsDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralSmsService(unitOfWork).EliminarCampaniaGeneralSms(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarActivarMasivoPorCampania(ActualizarActivarMasivoPorCampaniaSmsDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralSmsService(unitOfWork).ActualizarActivarMasivoPorCampania(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult EliminarCampaniaGeneralDetalleSms(EliminarCampaniaGeneralDetalleSmsDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralSmsService(unitOfWork).EliminarCampaniaGeneralDetalleSms(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarCampaniaGeneralDetalleSms(InsertarCampaniaGeneralDetalleSmsDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralSmsService(unitOfWork).InsertarCampaniaGeneralDetalleSms(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarCampaniaGeneralDetalleExcelSms(InsertarCampaniaGeneralDetalleSmsDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralSmsService(unitOfWork).InsertarCampaniaGeneralDetalleExcelSms(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarCamposCampaniaGeneralDetalleSms(ActualizarCamposCampaniaGeneralDetalleSmsDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralSmsService(unitOfWork).ActualizarCamposCampaniaGeneralDetalleSms(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult ProcesarDataPorPrioridadSendinblue(ProcesarDataPorPrioridadSendinblueSmsDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralSmsService(unitOfWork).ProcesarDataPorPrioridadSendinblue(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult ProcesarDataPorPrioridadExcel(ProcesarDataPorPrioridadExcelAlumnoSmsDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralSmsService(unitOfWork).ProcesarDataPorPrioridadExcel(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerConfiguracionCampaniaGeneralDetalleSms(IdDTO json)
        {
            try
            {
                return Ok(new CampaniaGeneralSmsService(unitOfWork).ObtenerConfiguracionCampaniaGeneralDetalleSms(json));
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
                return Ok(new CampaniaGeneralSmsService(unitOfWork).ObtenerCampaniaGeneralDetalleResponsablePorPrioridad(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult EliminarCampaniaGeneralDetalleResponsableSms(EliminarCampaniaGeneralDetalleResponsableSmsDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;
                return Ok(new CampaniaGeneralSmsService(unitOfWork).EliminarCampaniaGeneralDetalleResponsableSms(json));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarCampaniaGeneralDetalleResponsableSms(InsertarCampaniaGeneralDetalleResponsableSmsDTO json)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                json.Usuario = Usuario;

                return Ok(new CampaniaGeneralSmsService(unitOfWork).InsertarCampaniaGeneralDetalleResponsableSms(json, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerComboCampaniaGeneralDetalleResponsableSms()
        {
            try
            {
                return Ok(new CampaniaGeneralSmsService(unitOfWork).ObtenerComboCampaniaGeneralDetalleResponsableSms());
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
                return Ok(new CampaniaGeneralSmsService(unitOfWork).ObtenerComboCampaniasSendinBlue());
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
                return Ok(new CampaniaGeneralSmsService(unitOfWork).ObtenerComboCentroCostoCampaniasSendinBlue());
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
                return Ok(new CampaniaGeneralSmsService(unitOfWork).ReporteInteraccionCampaniaGeneralDetalle(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerPlantillaSms()
        {
            try
            {
                return Ok(new CampaniaGeneralSmsService(unitOfWork).ObtenerPlantillaSms());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarPlantillaSms(PlantillaSmsDTO datos)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                datos.Usuario = Usuario;
                return Ok(new CampaniaGeneralSmsService(unitOfWork).InsertarPlantillaSms(datos));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarDetalllePlantillaSms(DetallePlantillaSmsDTO datos)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                datos.Usuario = Usuario;
                return Ok(new CampaniaGeneralSmsService(unitOfWork).InsertarDetalllePlantillaSms(datos));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult EjecutarCampaniaGeneralEnvioSms()
        {
            try
            {
                return Ok(new CampaniaGeneralSmsService(unitOfWork).EjecutarCampaniaGeneralEnvioSms());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerPlantilla()
        {
            try
            {
                return Ok(new CampaniaGeneralSmsService(unitOfWork).ObtenerPlantilla());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerDetallePlantilla(IdDTO id)
        {
            try
            {
                return Ok(new CampaniaGeneralSmsService(unitOfWork).ObtenerDetallePlantilla(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarPlantillaSms(ActualizarPlantillaSmsDTO datos)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                datos.Usuario = Usuario;
                return Ok(new CampaniaGeneralSmsService(unitOfWork).ActualizarPlantillaSms(datos));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult EliminarPlantillaSms(IdDTO datos)
        {
            try
            {

                return Ok(new CampaniaGeneralSmsService(unitOfWork).EliminarPlantillaSms(datos));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult GenerarUrlFormulariosSmsLink(GenerarFormularioDTO datos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                var servicio = new CampaniaGeneralSmsService(unitOfWork);
                return Ok(servicio.GenerarUrlFormulariosLink(datos, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult EnviarPruebaSms(PruebaPlantillaSmsDTO datos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                var servicio = new CampaniaGeneralSmsService(unitOfWork);
                return Ok(servicio.EnviarSmsPrueba(datos, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerGrillaSms(tabGrillaSms datos)
        {
            try
            {

                return Ok(new CampaniaGeneralSmsService(unitOfWork).ObtenerGrillaSms(datos.tab, datos.dia));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerChatPorAlumno(StringDTO datos)
        {
            try
            {

                return Ok(new CampaniaGeneralSmsService(unitOfWork).ObtenerChatPorAlumno(datos.Valor));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ObtenerAlumnoPorCelular(StringDTO datos)
        {
            try
            {

                return Ok(new CampaniaGeneralSmsService(unitOfWork).ObtenerAlumnoPorCelular(datos.Valor));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult EnviarRespuestaSms(PruebaPlantillaSmsDTO datos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var Usuario = claimsIdentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                var servicio = new CampaniaGeneralSmsService(unitOfWork);
                return Ok(servicio.EnviarMensajeUnitario(datos, Usuario));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }

}









