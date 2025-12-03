using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.Wolkbox.WolkboxAgent;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.LinkedIn;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.LinkedIn;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion.Wolkbox;
using BSI.Integra.Aplicacion.Servicios.Service.Interface.Wolkbox;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BSI.Integra.Servicios.Controllers.Marketing.LinkedIn
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class LinkedInApiController : ControllerBase
    {
        private ILinkedInApiService _linkedInApiService;
        private ITokenManager _tokenManager;

        public LinkedInApiController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _linkedInApiService = new LinkedInApiService(unitOfWork);
            _tokenManager = tokenManager;
        }
   
        [HttpGet("[action]")]
        public async Task<IActionResult> ObtenerDatos()
        {
            var resultado = await _linkedInApiService.ObtenerDatos();
            return Ok(resultado);
        }

        [HttpGet("[action]")]
        public IActionResult ObtenerReporte()
        {
            var resultado = _linkedInApiService.ObtenerReporteLeads();
            return Ok(resultado);
        }


        [HttpPost("[action]")]
        public IActionResult ObtenerReporteLeadsByFecha([FromBody] FiltroLandingPagePortaLinkedInDTO filtro)
        {
            var resultado = _linkedInApiService.ObtenerReporteLeadsByFecha(filtro);
            return Ok(resultado);
        }


        [HttpGet("[action]/{cuentaAsociada}")]
        public IActionResult ObtenerReportePendientes(int cuentaAsociada)
        {
            var resultado = _linkedInApiService.ObtenerReportePendientes(cuentaAsociada);
            return Ok(resultado);

        }
        [Authorize]
        [JwtExpirationValidation]
        [HttpPut("[action]")]
        public IActionResult Actualizar([FromBody] LinkedInActualizarDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = _linkedInApiService.Actualizar(dto, _tokenManager.UserName);
            return Ok(respuesta);
        }

        [HttpGet("[action]")]
        public IActionResult SubirOportunidadesPendientes()
        {

            var resultado = _linkedInApiService.SubirOportunidadesPendientes(_tokenManager.UserName);
            return Ok(resultado);
        }
        [HttpPost("[action]")]
        public IActionResult SubirOportunidadesPendientesSeleccionadas([FromBody] SubirPendientesAgrupadas guidLinkedinLead)
        {
            var resultado = _linkedInApiService.SubirOportunidadesPendientesSeleccionadas(guidLinkedinLead,_tokenManager.UserName);

            return Ok(resultado);
        }

        [HttpGet("[action]")]
        public IActionResult ValidarCreacionOportunidadLinkedinEstado()
        {

            var resultado = _linkedInApiService.ValidarCreacionOportunidadLinkedinEstado();
            return Ok(resultado.Valor);
        }

        [HttpGet("[action]")]
        public IActionResult ValidarObtencionLeadLinkedinEstado()
        {

            var resultado = _linkedInApiService.ValidarEstadoParaControlLinkedin();
            return Ok(resultado.Valor);
        }
        [HttpGet("[action]/{cuentaAsociada}")]
        public IActionResult ValidarObtencionLeadLinkedinEstadoPorCuenta(int cuentaAsociada)
        {

            var resultado = _linkedInApiService.ValidarEstadoParaControlLinkedinPorCuenta(cuentaAsociada);
            return Ok(resultado);
        }

        [HttpGet("[action]")]
        public IActionResult ObtenerCuentasActivas()
        {

            var resultado = _linkedInApiService.ObtenerCuentasActivas();
            return Ok(resultado);
        }
    }
}
