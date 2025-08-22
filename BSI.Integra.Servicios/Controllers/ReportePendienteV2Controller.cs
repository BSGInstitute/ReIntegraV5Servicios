using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReportePendienteV2Controller
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 18/09/2023
    /// <summary>
    /// Gestión de ReportePendienteV2
    /// </summary>
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReportePendienteV2Controller : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReportePendienteV2Controller(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// TipoFuncion: POST
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 18/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los comprobantes por tipo asociado
        /// </summary>
        /// <param></param>
        [Route("ObtenerReportePendienteV2")]
        [HttpPost]
        public ActionResult ObtenerReportePendienteV2(ReportePendientePeriodoFiltroDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReportePendienteV2Service(unitOfWork);
                return Ok(servicio.GenerarReportePeriodo(filtro));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 18/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los comprobantes por tipo asociado
        /// </summary>
        /// <param></param>
        [Route("ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo")]
        [HttpPost]
        public ActionResult ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo(ReportePendientePeriodoFiltroPruebaDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReportePendienteV2Service(unitOfWork);
                return Ok(servicio.ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo(filtro));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 18/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los comprobantes por tipo asociado
        /// </summary>
        /// <param></param>
        [Route("ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo_Matriculados")]
        [HttpPost]
        public ActionResult ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo_Matriculados(ReportePendientePeriodoFiltroPruebaDTO filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReportePendienteV2Service(unitOfWork);
                return Ok(servicio.ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo_Matriculados(filtro));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 18/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los comprobantes por tipo asociado
        /// </summary>
        /// <param></param>
        [Route("ObtenerReportePendienteCierrePorPeriodo")]
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtenerReportePendienteCierrePorPeriodo (ReportePendientePeriodoFiltroPruebaDTO filtroPendiente)
        {
            var servicio = new ReportePendienteV2Service(unitOfWork);
            filtroPendiente.Identificador = Guid.NewGuid(); 
            servicio.ObtenerReportePendienteCierrePorPeriodo(filtroPendiente);
            return Ok(filtroPendiente.Identificador);
        }



        /// TipoFuncion: POST
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 18/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los comprobantes por tipo asociado
        /// </summary>
        /// <param></param>
        [Route("ObtenerReportePendienteCierrePorPeriodoPrueba")]
        [HttpPost]
        public ActionResult ObtenerReportePendienteCierrePorPeriodoPrueba(StringDTO dato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReportePendienteV2Service(unitOfWork);
                return Ok(servicio.ObtenerReportePendienteCierrePorPeriodoPrueba(dato));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 18/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los comprobantes por tipo asociado
        /// </summary>
        /// <param></param>
        [Route("GenerarReportePendientePorPeriodoOperacionesGeneralPrueba")]
        [HttpPost]
        public ActionResult GenerarReportePendientePorPeriodoOperacionesGeneralPrueba(EnvioDarosPruebaDTO datos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReportePendienteV2Service(unitOfWork);
                return Ok(servicio.GenerarReportePendientePorPeriodoOperacionesGeneralPrueba(datos));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// TipoFuncion: POST
        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 18/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los comprobantes por tipo asociado
        /// </summary>
        /// <param></param>
        [Route("GenerarReportePeriodo")]
        [HttpPost]
        public ActionResult GenerarReportePeriodo(ReportePendientePeriodoFiltroDTO FiltroPendiente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new ReportePendienteV2Service(unitOfWork);
                return Ok(servicio.GenerarReportePeriodo(FiltroPendiente));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Route("[action]")]
        //[HttpGet]
        //public ActionResult ObtenerCombos()
        //{
        //    try
        //    {
        //        PersonalService servicioPersonal = new PersonalService(unitOfWork);
        //        ReporteTasaConversionConsolidadaDTO resultado = new ReporteTasaConversionConsolidadaDTO();

        //        resultado.Asesores = servicioPersonal.ObtenerPersonalAsesoresFiltro();
        //        resultado.Coordinadores = servicioPersonal.ObtenerPersonalCoordinadoresFiltro();
        //        return Ok(resultado);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}


        //[Route("[action]")]
        //[HttpGet]
        //public ActionResult ObtenerFrecuenciaReportePendienteV2()
        //{
        //    try
        //    {
        //        var servicio = new FrecuenciaService(unitOfWork);
        //        return Ok(servicio.ObtenerFrecuenciaReportePendienteV2());
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}



    }
}
