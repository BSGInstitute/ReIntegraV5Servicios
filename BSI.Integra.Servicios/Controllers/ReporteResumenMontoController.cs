using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteResumenMontoController
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión de ReporteResumenMonto
    /// </summary>
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteResumenMontoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public ReporteResumenMontoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: POST
        /// Autor: Griselberto H.
        /// Fecha: 11/01/2023
        /// <summary>
        /// Genera reporte general.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns> DTO: reporte - ReportePendienteCompuestoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerReporteResumenMontosCierre([FromBody] ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                var reportePendienteService = new ReporteResumenMontoService(unitOfWork);
                var reporte = reportePendienteService.ObtenerReporteResumenMontosCierre(FiltroPendiente);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Griselberto H.
        /// Fecha: 11/01/2023
        /// <summary>
        /// Genera reporte general.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns> DTO: reporte - ReportePendienteCompuestoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerReporteResumenMontosNuevosMatriculados([FromBody] ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                var reportePendienteService = new ReporteResumenMontoService(unitOfWork);
                var reporte = reportePendienteService.ObtenerReporteResumenMontosNuevosMatriculados(FiltroPendiente);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Griselberto H.
        /// Fecha: 11/01/2023
        /// <summary>
        /// Genera reporte general.
        /// </summary>
        /// <returns> DTO: reporte - ReportePendienteCompuestoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerReporteResumenMontosDiferencias([FromBody] ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                var reportePendienteService = new ReporteResumenMontoService(unitOfWork);
                var reporte = reportePendienteService.ObtenerReporteResumenMontosDiferencias(FiltroPendiente);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Griselberto H.
        /// Fecha: 11/01/2023
        /// <summary>
        /// Genera reporte general.
        /// </summary>
        /// <returns> DTO: reporte - ReportePendienteCompuestoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerReporteResumenMontos([FromBody] ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                var reportePendienteService = new ReporteResumenMontoService(unitOfWork);
                var reporte = reportePendienteService.ObtenerReporteResumenMontos(FiltroPendiente);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor: Griselberto H.
        /// Fecha: 11/01/2023
        /// <summary>
        /// Genera reporte general.
        /// </summary>
        /// <returns> DTO: reporte - ReportePendienteCompuestoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerReporteResumenMontosCambios([FromBody] ReporteResumenMontosFiltroDTO FiltroPendiente)
        {
            try
            {
                var reportePendienteService = new ReporteResumenMontoService(unitOfWork);
                var reporte = reportePendienteService.ObtenerReporteResumenMontosCambios(FiltroPendiente);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Griselberto H.
        /// Fecha: 11/01/2023
        /// <summary>
        /// Genera reporte general.
        /// </summary>
        /// <returns> DTO: reporte - ReportePendienteCompuestoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteResumenMontosTotalizadoPeriodoActual([FromBody] ReporteResumenMontosGeneralTotalDTO FiltroPendiente)
        {
            try
            {
                var reportePendienteService = new ReporteResumenMontoService(unitOfWork);
                var reporte = reportePendienteService.GenerarReporteResumenMontosTotalizadoPeriodoActual(FiltroPendiente);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor: Griselberto H.
        /// Fecha: 11/01/2023
        /// <summary>
        /// Genera reporte general.
        /// </summary>
        /// <returns> DTO: reporte - ReportePendienteCompuestoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteResumenMontosTotalizadoPeriodoCierre([FromBody] ReporteResumenMontosGeneralTotalDTO FiltroPendiente)
        {
            try
            {
                var reportePendienteService = new ReporteResumenMontoService(unitOfWork);
                var reporte = reportePendienteService.GenerarReporteResumenMontosTotalizadoPeriodoCierre(FiltroPendiente);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Griselberto H.
        /// Fecha: 11/01/2023
        /// <summary>
        /// Genera reporte general.
        /// </summary>
        /// <returns> DTO: reporte - ReportePendienteCompuestoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteResumenMontosVariacionMensual([FromBody] ReporteResumenMontosGeneralTotalDTO FiltroPendiente)
        {
            try
            {
                var reportePendienteService = new ReporteResumenMontoService(unitOfWork);
                var reporte = reportePendienteService.GenerarReporteResumenMontosVariacionMensual(FiltroPendiente);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Griselberto H.
        /// Fecha: 11/01/2023
        /// <summary>
        /// Genera reporte general.
        /// </summary>
        /// <returns> DTO: reporte - ReportePendienteCompuestoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteResumenMontosNuevosMatriculados([FromBody] ReporteResumenMontosGeneralTotalDTO FiltroPendiente)
        {
            try
            {
                var reportePendienteService = new ReporteResumenMontoService(unitOfWork);
                var reporte = reportePendienteService.GenerarReporteResumenMontosNuevosMatriculados(FiltroPendiente);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor: Griselberto H.
        /// Fecha: 11/01/2023
        /// <summary>
        /// Genera reporte general.
        /// </summary>
        /// <returns> DTO: reporte - ReportePendienteCompuestoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteResumenMontosTotalizadoPais([FromBody] ReporteResumenMontosGeneralTotalDTO FiltroPendiente)
        {
            try
            {
                var reportePendienteService = new ReporteResumenMontoService(unitOfWork);
                var reporte = reportePendienteService.GenerarReporteResumenMontosTotalizadoPais(FiltroPendiente);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Griselberto H.
        /// Fecha: 11/01/2023
        /// <summary>
        /// Genera reporte general.
        /// </summary>
        /// <returns> DTO: reporte - ReportePendienteCompuestoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteResumenMontosTotalizadoModalidadPresencialPais([FromBody] ReporteResumenMontosGeneralTotalDTO FiltroPendiente)
        {
            try
            {
                var reportePendienteService = new ReporteResumenMontoService(unitOfWork);
                var reporte = reportePendienteService.GenerarReporteResumenMontosTotalizadoModalidadPresencialPais(FiltroPendiente);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Griselberto H.
        /// Fecha: 11/01/2023
        /// <summary>
        /// Genera reporte general.
        /// </summary>
        /// <returns> DTO: reporte - ReportePendienteCompuestoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteResumenMontosTotalizadoModalidadOnlinePais([FromBody] ReporteResumenMontosGeneralTotalDTO FiltroPendiente)
        {
            try
            {
                var reportePendienteService = new ReporteResumenMontoService(unitOfWork);
                var reporte = reportePendienteService.GenerarReporteResumenMontosTotalizadoModalidadOnlinePais(FiltroPendiente);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Griselberto H.
        /// Fecha: 11/01/2023
        /// <summary>
        /// Genera reporte general.
        /// </summary>
        /// <returns> DTO: reporte - ReportePendienteCompuestoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteResumenMontosTotalizadoModalidadAonlinePais([FromBody] ReporteResumenMontosGeneralTotalDTO FiltroPendiente)
        {
            try
            {
                var reportePendienteService = new ReporteResumenMontoService(unitOfWork);
                var reporte = reportePendienteService.GenerarReporteResumenMontosTotalizadoModalidadAonlinePais(FiltroPendiente);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Griselberto H.
        /// Fecha: 11/01/2023
        /// <summary>
        /// Genera reporte general.
        /// </summary>
        /// <returns> DTO: reporte - ReportePendienteCompuestoDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteResumenMontosTotalizadoModalidadInHousePais([FromBody] ReporteResumenMontosGeneralTotalDTO FiltroPendiente)
        {
            try
            {
                var reportePendienteService = new ReporteResumenMontoService(unitOfWork);
                var reporte = reportePendienteService.GenerarReporteResumenMontosTotalizadoModalidadInHousePais(FiltroPendiente);
                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    
}
