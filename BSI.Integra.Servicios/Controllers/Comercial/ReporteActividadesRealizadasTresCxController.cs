using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace BSI.Integra.Servicios.Controllers.Comercial
{
    [Route("api/ReporteActividadesRealizadasTresCx")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteActividadesRealizadasTresCxController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private IReporteActividadesRealizadasTresCxService _reporteActividadesRealizadasTresCxService;
        public ReporteActividadesRealizadasTresCxController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            _reporteActividadesRealizadasTresCxService = new ReporteActividadesRealizadasTresCxService(unitOfWork);
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReporteActividadesRealizadasFiltrosDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _reporteActividadesRealizadasTresCxService.ReporteActividadesRealizadas(filtro);
            return Ok(resultado);
        }
    }
}
