using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReportePagosDiaPeriodoController
    /// Autor: Jonathan Caipo.
    /// Fecha: 13/01/2023
    /// <summary>
    /// Gestión de Pagos por día periodo.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReportePagosDiaPeriodoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;

        public ReportePagosDiaPeriodoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 13/01/2023
        /// Version: 1.0
        /// <summary>
        /// Genera reporte completo.
        /// </summary>
        /// <param name="filtroReportePagosDiaPeriodo"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReportePagosDiaPeriodoFiltroDTO filtroReportePagosDiaPeriodo)
        {
            try
            {
                CronogramaPagoDetalleFinalService reporteCronogramaGeneral = new CronogramaPagoDetalleFinalService(unitOfWork);
                var respuestaGeneral = reporteCronogramaGeneral.GenerarReportePagosDiaPeriodoGeneral(filtroReportePagosDiaPeriodo);

                CronogramaPagoDetalleFinalService reportePagosPorDia = new CronogramaPagoDetalleFinalService(unitOfWork);
                var reportePagosPorDiaFinal = reporteCronogramaGeneral.GenerarReportePagosPorDia(respuestaGeneral);


                CronogramaPagoDetalleFinalService reportePagosPorPeriodo = new CronogramaPagoDetalleFinalService(unitOfWork);
                var reportePagosPorPeriodoFinal = reporteCronogramaGeneral.GenerarReportePagosPorPeriodo(respuestaGeneral);

                ReportePagosDiaPeriodoCompuestoDTO reporte = new ReportePagosDiaPeriodoCompuestoDTO();
                reporte.ReportePagosPorDia = reportePagosPorDiaFinal.ToList();
                reporte.ReportePagosPorPeriodo = reportePagosPorPeriodoFinal.ToList();

                return Ok(reporte);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 13/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene combo de pagos por dia y periodo.
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <returns></returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombosPagosDiaPeriodo(int idPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PeriodoService periodoService = new PeriodoService(unitOfWork);
                PersonalService personalService = new PersonalService(unitOfWork);

                CombosPagosDiaPeriodoDTO comboPendiente = new CombosPagosDiaPeriodoDTO();
                comboPendiente.ListaPeriodo = periodoService.ObtenerPeriodosPendiente();

                List<PersonalAsignadoDTO> asistentes = personalService.ObtenerPersonalAsignadoOperacionesUsuarioTotal(idPersonal);
                //activos
                comboPendiente.AsistentesActivos = asistentes.Where(w => w.Activo == true).ToList();
                //todos
                comboPendiente.AsistentesTotales = asistentes;
                //inactivo
                comboPendiente.AsistentesInactivos = asistentes.Where(w => w.Activo == false).ToList();

                return Ok(comboPendiente);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
