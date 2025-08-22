using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICronogramaPagoDetalleFinalService
    {
        #region Metodos Base
        CronogramaPagoDetalleFinal Add(CronogramaPagoDetalleFinal entidad);
        CronogramaPagoDetalleFinal Update(CronogramaPagoDetalleFinal entidad);
        bool Delete(int id, string usuario);

        List<CronogramaPagoDetalleFinal> Add(List<CronogramaPagoDetalleFinal> listadoEntidad);
        List<CronogramaPagoDetalleFinal> Update(List<CronogramaPagoDetalleFinal> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<CronogramaPagoDetalleFinalDTO> ObtenerCronogramaPagoDetalleFinal();
        IEnumerable<CronogramaPagoDetalleFinalComboDTO> ObtenerCombo();
        IEnumerable<CronogramaPagoDetalleFinalCuotaDTO> ObtenerListaCuotaPorIdMatriculaCabecera(int idMatriculaCabecera);
        List<CronogramaPagoDetalleFinalFinanzasDTO> ObtenerCronogramaFinanzasPorVersionYMCabecera(int version, int idMatriculaCabecera);
        IEnumerable<CronogramaPagoDetalleFinalDTO> ObtenerCronograma(int idMatriculaCabecera);
        List<ProgramaListaCuotaDTO> ObtenerListaCuotaPrograma(int idMatricula);
        List<ResultadoFechaCompromiso> ObtenerVersionesFechaCompromiso(int idCuota);
        bool ActualizarComprobantePago(ActualizarComprobantePagoAlumnoDTO data);
        ReportePendienteGeneralDTO GenerarReportePendienteOperacionesGeneral(ReportePendienteFiltroDTO filtroPendiente);
        IList<ReportePendienteDetalleFinalDTO> GenerarReportePendientePorPeriodoOperaciones(ReportePendienteGeneralDTO respuestaGeneral);
        IList<ReportePendienteDetalleFinalDTO> GenerarReportePendienteIngresoVentasPorPeriodoOperacionesAnterior(ReportePendienteGeneralDTO respuestaGeneral);
        IList<ReportePendienteDetalleFinalDTO> GenerarReportePendientePorCoordinadoraOperaciones(ReportePendienteGeneralDTO respuestaGeneral);
        IList<ReportePendienteDetalleFinalPorCoordinadorDTO> GenerarReportePendientePeriodoYCoordinadorOperaciones(ReportePendienteGeneralDTO respuestaGeneral);
        PagosDiaPeriodoGeneralDTO GenerarReportePagosDiaPeriodoGeneral(ReportePagosDiaPeriodoFiltroDTO FiltroReportePagosDiaPeriodo);
        IEnumerable<ReportePagosDiaPeriodoAgrupadoDTO> GenerarReportePagosPorDia(PagosDiaPeriodoGeneralDTO respuestaGeneral);
        IEnumerable<ReportePagosDiaPeriodoAgrupadoDTO> GenerarReportePagosPorPeriodo(PagosDiaPeriodoGeneralDTO respuestaGeneral);
        List<CronogramaPagoDetalleFinalDTO> ObtenerCronogramaFinanzas(int version, int idMatriculaCabecera);
        public byte[] GenerarCrep(CrepCabeceraDTO objeto, List<CrepListaCuotasSeleccionadasDTO> lista, List<CrepListaAlumnosDTO> listalumnos);

        public bool CambiarFechaProcesos(ListaEnterosDTO listaEnteros);
        public bool CambiarFechaProcesoCronograma(FechaCronogramaDTO data);
        public object ObtenerCronogramaFinal(int idMatriculaCabecera);
        IEnumerable<DetalleCuotasTransaccionAuditoriaDTO> ObtenerDetalleCuotasTransaccionAuditoria(FiltroDetalleCuotasTransaccionAuditoriaDTO FiltroDetalle);
        IEnumerable<DetalleMatriculaTransaccionAuditoriaDTO> ObtenerDetalleMatriculaTransaccionAuditoria(FiltroDetalleMatriculaTransaccionAuditoriaDTO FiltroDetalle);
        bool ActualizaEnviadoSiigo(int id);
    }
}
