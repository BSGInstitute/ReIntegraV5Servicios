using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Comercial
{


    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteCambioDeFaseTresCxController : ControllerBase
    {

        private IUnitOfWork unitOfWork;
        private IReporteCambioDeFaseTresCxService _reporteCambiodeFaseTresCxService;
        public ReporteCambioDeFaseTresCxController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            _reporteCambiodeFaseTresCxService = new ReporteCambioDeFaseTresCxService(unitOfWork);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera el reporte de tasa de contacto
        /// </summary>
        /// <param></param>
        /// <returns> ReporteCambioDeFaseTasaContactoDTO </returns>
        [Route("GenerarReporteTasaContactoTresCxAsync")]
        [HttpPost]
        public async Task<IActionResult> GenerarReporteTasaContactoTresCxAsync([FromBody] ReporteCambioFaseFiltrosDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = await _reporteCambiodeFaseTresCxService.GenerarReporteTasaContactoTresCxAsync(filtro);
            return Ok(resultado);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera el reporte de tasa de contacto sin restriccion de llamadas
        /// </summary>
        /// <param></param>
        /// <returns> ReporteCambioDeFaseTasaContactoDTO </returns>
        [Route("GenerarReporteTasaContactoTresCxV2Async")]
        [HttpPost]
        public async Task<IActionResult> GenerarReporteTasaContactoTresCxV2Async([FromBody] ReporteCambioFaseFiltrosDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = await _reporteCambiodeFaseTresCxService.GenerarReporteTasaContactoTresCxTotalAsync(filtro);
            return Ok(resultado);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera el reporte de tasa de contacto por otro medio
        /// </summary>
        /// <param></param>
        /// <returns> ReporteCambioDeFaseTasaContactoDTO </returns>
        [Route("GenerarReporteTasaContactoTresCxOtroMedioAsync")]
        [HttpPost]
        public async Task<IActionResult> GenerarReporteTasaContactoTresCxOtroMedioAsync([FromBody] ReporteCambioFaseFiltrosDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = await _reporteCambiodeFaseTresCxService.GenerarReporteTasaContactoTresCxOtroMedioAsync(filtro);
            return Ok(resultado);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/12/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera el reporte de control de actividades
        /// </summary>
        /// <param></param>
        /// <returns> objeto DTO : ReporteCambioDeFaseCombosGeneralDTO </returns>
        [Route("GenerarReporteV2Async")]
        [HttpPost]
        public async Task<IActionResult> GenerarReporteV2Async([FromBody] ReporteCambioFaseFiltrosDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reporteCambioDeFaseDataDTO = await _reporteCambiodeFaseTresCxService.ReporteCambioDeFaseV2Async(filtro);
            return Ok(reporteCambioDeFaseDataDTO);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 6/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Calidad procesamiento
        /// </summary>
        /// <returns> objeto DTO : ReporteCalidadCambioDeFaseDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerReporteConteoDatosFaseAlterno([FromBody] ReporteCambioFaseFiltrosDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IReporteCambiodeFaseService _reporteCambiodeFaseService = new ReporteCambiodeFaseService(unitOfWork);
            var resultado = _reporteCambiodeFaseService.ObtenerReporteConteoDatosFaseAlterno(filtro);
            return Ok(resultado);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 6/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Calidad procesamiento
        /// </summary>
        /// <returns> objeto DTO : ReporteCalidadCambioDeFaseDTO </returns>
        [Route("GenerarReporteV2ControlBICYEAcumuladoAsync")]
        [HttpPost]
        public async Task<IActionResult> GenerarReporteV2ControlBICYEAcumuladoAsync([FromBody] ReporteCambioFaseFiltrosDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IReporteCambiodeFaseService _reporteCambiodeFaseService = new ReporteCambiodeFaseService(unitOfWork);
                var resultado = await _reporteCambiodeFaseService.ObtenerReporteCambiosDeFaseControlBICYEAcumuladoAsync(filtro);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 6/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Calidad procesamiento
        /// </summary>
        /// <returns> objeto DTO : ReporteCalidadCambioDeFaseDTO </returns>
        [Route("GenerarReporteV2IntegraAsync")]
        [HttpPost]
        public async Task<ActionResult> GenerarReporteV2IntegraAsync([FromBody] ReporteCambioFaseFiltrosDTO filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = await _reporteCambiodeFaseTresCxService.ReporteCambioDeFaseV2IntegraAsync(filtros);
            return Ok(resultado);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 6/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Calidad procesamiento
        /// </summary>
        /// <returns> objeto DTO : ReporteCalidadCambioDeFaseDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteCalidadProcesamiento([FromBody] ReporteCambioFaseFiltrosDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IReporteService servicioReporte = new ReporteService(unitOfWork);
            var resultado = servicioReporte.ReporteCalidadCambioDeFaseAlterno(filtro);
            return Ok(resultado);

        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 6/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Calidad procesamiento
        /// </summary>
        /// <returns> objeto DTO : ReporteCalidadCambioDeFaseDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteActividadEjecutadaLlamadaObservada([FromBody] ReporteCambioFaseFiltrosDTO filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_reporteCambiodeFaseTresCxService.ObtenerReporteActividadEjecutadaLlamadaObservada(filtros));
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Calidad procesamiento
        /// </summary>
        /// <returns> objeto DTO : ReporteCalidadCambioDeFaseDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteActividadEjecutadaLlamadaObservadaV2([FromBody] ReporteCambioFaseFiltrosDTO filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_reporteCambiodeFaseTresCxService.ObtenerReporteActividadEjecutadaLlamadaObservadaV2(filtros));
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 6/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Calidad procesamiento
        /// </summary>
        /// <returns> objeto DTO : ReporteCalidadCambioDeFaseDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarAcumuladoTiempoContactoEfectivo([FromBody] ReporteCambioFaseFiltrosDTO filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_reporteCambiodeFaseTresCxService.ObtenerAcumuladoTiempoContactoEfectivo(filtros));
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 6/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Calidad procesamiento
        /// </summary>
        /// <returns> objeto DTO : ReporteCalidadCambioDeFaseDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarAcumuladoLlamadasReprogramadasManualmente([FromBody] ReporteCambioFaseFiltrosDTO filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_reporteCambiodeFaseTresCxService.ObtenerAcumuladoLlamadasReprogramadasManualmente(filtros));
        }
        /// TipoFuncion: POST
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 6/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Calidad procesamiento
        /// </summary>
        /// <returns> objeto DTO : ReporteCalidadCambioDeFaseDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarActividadEjecutadaFaseActual([FromBody] ReporteCambioFaseFiltrosDTO filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_reporteCambiodeFaseTresCxService.ObtenerActividadEjecutadaFaseActualTresCx(filtros));
        }

        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 26/02/2024
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de control de oportunidades predictivas
        /// </summary>
        /// <returns> objeto DTO : ReporteCalidadCambioDeFaseDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerControlOportunidadPredictiva([FromBody] ReporteCambioFaseFiltrosDTO filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_reporteCambiodeFaseTresCxService.ObtenerControlOportunidadPredictiva(filtros));
        }
    }
}
