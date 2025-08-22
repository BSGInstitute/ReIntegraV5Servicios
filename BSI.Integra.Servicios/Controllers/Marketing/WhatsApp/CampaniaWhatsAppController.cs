using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace BSI.Integra.Servicios.Controllers.Marketing.WhatsApp
{
    [Route("api/[controller]")]
    [ApiController]

    public class CampaniaWhatsAppController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CampaniaWhatsAppController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("Obtener/{idCampaniaGeneral}/{idCampaniageneralDetalle}")]
        [HttpGet]
        public IActionResult ObtenerConfiguracionDeEnvioWpp(int idCampaniaGeneral,int idCampaniageneralDetalle)
        {
            return Ok(new CampaniaWhatsAppService(unitOfWork).ObtenerPrioridadesDeFiltroWpp(idCampaniaGeneral,idCampaniageneralDetalle));
        }
        [Route("MigrarData/Plantilla/{IdCampaniaGeneral}")]
        [HttpGet]
        public IActionResult GenerarDataParaWhatsAppConfiguracionPreEnvio(int IdCampaniaGeneral)
        {
            try
            {
                return Ok(new CampaniaWhatsAppService(unitOfWork).GenerarDataParaWhatsAppConfiguracionPreEnvio(IdCampaniaGeneral));
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
