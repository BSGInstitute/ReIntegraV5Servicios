using BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.Marketing.Melissa;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Melissa;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.LinkedIn;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.Melissa.MelissaDTO;

namespace BSI.Integra.Servicios.Controllers.Marketing.MelissaGlobalPhoneVerify
{
    /// Controlador: WolkboxController
    /// Autor: Joseph Llanque
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de Wolkbox
    /// </summary>
    [Route("api/Melissa")]
    [ApiController]
    public class MelissaController :  ControllerBase
    {
        //private IMelissaService _melissaService;
        private IUnitOfWork unitOfWork;
        HttpClient httpClient = new HttpClient();
        public MelissaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("[action]/{numero}/{idCodigoPais}")]
        [HttpGet]
        public async Task<IActionResult> ObtenerNumero(string numero,int? idCodigoPais)
        {
            var _melissaService = new MelissaService(unitOfWork, httpClient);
            var resultado = await _melissaService.ValidarNumero(numero, idCodigoPais);
            return Ok(resultado);
        }
    }
}
