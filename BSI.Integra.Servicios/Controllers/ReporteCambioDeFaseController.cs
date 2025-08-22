using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteCambioDeFaseController
    /// Autor: Gilmer Quispe
    /// Fecha: 22/09/2022
    /// <summary>
    /// Gestión Reporte de Cambio de Fase
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteCambioDeFaseController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private IReporteCambiodeFaseService _reporteCambiodeFaseService;
        private IConfiguracionAccesoPersonalService _configuracionAccesoPersonalService;
        public ReporteCambioDeFaseController(IUnitOfWork unitOfWork, IConfiguracionAccesoPersonalService configuracionAccesoPersonalService)
        {
            this.unitOfWork = unitOfWork;
            _reporteCambiodeFaseService = new ReporteCambiodeFaseService(unitOfWork);
            _configuracionAccesoPersonalService = configuracionAccesoPersonalService;
        }

        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 12/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Combos para Módulo de Reporte de Cambio de Fase
        /// </summary>
        /// <param></param>
        /// <returns> objeto DTO : ReporteCambioDeFaseCombosGeneralDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);

            var idPersonal = _configuracionAccesoPersonalService.ObtenerIdPersonalAcceso(_respuestaCorrecta.RegistroClaimToken.IdPersonal, "Comercial/ReporteCambioFase");

            if (_respuestaCorrecta.TokenValida)
            {
                try
                {
                    var servicioCentroCosto = new CentroCostoService(unitOfWork);
                    var servicioPersonal = new PersonalService(unitOfWork);

                    var resultado = new ReporteCambioDeFaseCombosGeneralDTO();
                    resultado.Asesores = servicioPersonal.ObtenerAsesoresVentasOficial_CF(idPersonal).Where(w => w.TipoPersonal == "Asesor" || w.TipoPersonal == "otro").ToList();
                    resultado.CentroCostos = servicioCentroCosto.ObtenerCombo();
                    return Ok(resultado);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
                throw new UnauthorizedAccessException("Usted no tiene acceso");
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer Quispe
        /// Fecha: 22/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene lista de centro de costos para filtro por Asesores
        /// </summary>
        /// <param name="idsAsesor">Ids del, o de los, asesor(es)</param>
        /// <returns> Lista objeto DTO : List<FiltroDTO> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCentroCostoPorPersonal([FromBody] ListadoIdDTO idsAsesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicioCentroCosto = new CentroCostoService(unitOfWork);
                string asesores = string.Join(",", idsAsesor.Ids);

                var resultado = servicioCentroCosto.ObtenerCentroCostoPorAsesores(idsAsesor.Ids);
                if (resultado != null)
                {
                    return Ok(resultado);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 14/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Cambio de Fase Versión 2
        /// </summary>
        /// <returns> objeto DTO : ReporteCambioDeFaseDataV2DTO </returns>
        [Route("GenerarReporteV2Async")]
        [HttpPost]
        public async Task<IActionResult> GenerarReporteV2Async([FromBody] ReporteCambioFaseFiltrosDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var reporteCambioDeFaseDataDTO = await _reporteCambiodeFaseService.ReporteCambioDeFaseV2Async(filtro);
                return Ok(reporteCambioDeFaseDataDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 14/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Cambio de Fase Versión 2
        /// </summary>
        /// <returns> objeto DTO : ReporteCambioDeFaseDataV2DTO </returns>
        [Route("GenerarReporteV2TasaContactoAsync")]
        [HttpPost]
        public async Task<IActionResult> GenerarReporteV2TasaContactoAsync([FromBody] ReporteCambioFaseFiltrosDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resultado = await _reporteCambiodeFaseService.GenerarReporteV2TasaContactoAsync(filtro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 14/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Cambio de Fase Versión 2
        /// </summary>
        /// <returns> objeto DTO : ReporteCambioDeFaseDataV2DTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteV2ControlBICYEAcumulado([FromBody] ReporteCambioFaseFiltrosDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resultado = _reporteCambiodeFaseService.ObtenerReporteCambiosDeFaseControlBICYEAcumulado(filtro);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 14/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Cambio de Fase Versión 2
        /// </summary>
        /// <returns> objeto DTO : ReporteCambioDeFaseDataV2DTO </returns>
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
                var resultado = await _reporteCambiodeFaseService.ObtenerReporteCambiosDeFaseControlBICYEAcumuladoAsync(filtro);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// TipoFuncion: POST
        /// Autor: Gilmer
        /// Fecha: 31/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Cambio de Fase en Integra
        /// </summary>
        /// <returns> objeto DTO : ReporteCambioDeFaseDataDTO </returns>
        [Route("GenerarReporteV2IntegraAsync")]
        [HttpPost]
        public async Task<ActionResult> GenerarReporteV2IntegraAsync([FromBody] ReporteCambioFaseFiltrosDTO filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resultado = await _reporteCambiodeFaseService.ReporteCambioDeFaseV2IntegraAsync(filtros);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 14/04/2023
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
            try
            {
                var servicioReporte = new ReporteService(unitOfWork);
                var resultado = servicioReporte.ReporteCalidadCambioDeFaseAlterno(filtro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 14/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Calidad procesamiento
        /// </summary>
        /// <returns> objeto DTO : ReporteCalidadCambioDeFaseDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerReporteConteoDatosFase([FromBody] ReporteCambioFaseFiltrosDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultado = _reporteCambiodeFaseService.ObtenerReporteConteoDatosFase(filtro);
            return Ok(resultado);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 11/10/2023
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte Conteo de Datos Fase
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
            var resultado = _reporteCambiodeFaseService.ObtenerReporteConteoDatosFaseAlterno(filtro);
            return Ok(resultado);
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 17/05/2023
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
            try
            {

                IReporteCambiodeFaseService service = new ReporteCambiodeFaseService(unitOfWork);
                return Ok(service.ObtenerReporteActividadEjecutadaLlamadaObservada(filtros));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 17/05/2023
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
            try
            {
                IReporteCambiodeFaseService service = new ReporteCambiodeFaseService(unitOfWork);
                return Ok(service.ObtenerAcumuladoTiempoContactoEfectivo(filtros));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 17/05/2023
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
            try
            {
                IReporteCambiodeFaseService service = new ReporteCambiodeFaseService(unitOfWork);
                return Ok(service.ObtenerAcumuladoLlamadasReprogramadasManualmente(filtros));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 17/05/2023
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
            try
            {
                IReporteCambiodeFaseService service = new ReporteCambiodeFaseService(unitOfWork);
                return Ok(service.ObtenerActividadEjecutadaFaseActual(filtros));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
