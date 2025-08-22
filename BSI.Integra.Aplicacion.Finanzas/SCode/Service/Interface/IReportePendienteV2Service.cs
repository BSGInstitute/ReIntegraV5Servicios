using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IReportePendienteV2Service
    {

        public ReportePendienteGeneralPeriodoDTO GenerarReportePendientePorPeriodoOperacionesGeneral(ReportePendientePeriodoFiltroPruebaDTO filtroPendiente);
        public IList<ReportePendienteDetalleFinalDTO> GenerarReportePendienteIngresoVentasPorPeriodoOperaciones(ReportePendienteGeneralPeriodoDTO respuestaGeneral);
        public IList<ReportePendienteDetalleFinalDTO> GenerarReportePendientePorCoordinadoraOperacionesPorPeriodo(ReportePendienteGeneralPeriodoDTO respuestaGeneral);

        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo(ReportePendientePeriodoFiltroPruebaDTO filtroPendiente);
        Task<bool> ObtenerReportePendienteCierrePorPeriodo(ReportePendientePeriodoFiltroPruebaDTO filtroPendiente);
        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo_Matriculados(ReportePendientePeriodoFiltroPruebaDTO filtroPendiente);

        public ReportePendienteCompuestoDTO GenerarReportePeriodo(ReportePendientePeriodoFiltroDTO FiltroPendiente);

        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendienteCierrePorPeriodoPrueba(StringDTO valor);


    }
}
