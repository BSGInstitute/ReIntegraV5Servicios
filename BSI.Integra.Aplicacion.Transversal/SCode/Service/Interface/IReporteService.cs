using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IReporteService
    {
        List<ReporteCalidadProcesamientoDTO> ReporteCalidadProcesamientoV2(ReporteCambioFaseFiltrosDTO filtros);
        List<DiferenciaLlamadasBloqueDTO> ReporteDiferenciaLlamadasBloque(ReporteCambioFaseFiltrosDTO filtros);
        ValorIntDTO AprobacionSolicitudVisualizacion(AprobacionSolicitudesVisualizacionFiltroDTO aprobacionFiltro);
        ReporteTasaConversionConsolidadaMensualGraficasVistaDTO ReporteTasaConversionConsolidadoAsesoresGraficaMensual(ReporteTasaConversionConsolidadaGraficaFiltroDTO filtro);
        ReporteTasaConversionConsolidadaGraficasVistaDTO ReporteTasaConversionConsolidadoAsesoresGrafica(ReporteTasaConversionConsolidadaGraficaFiltroDTO filtros);
        ReporteTasaConversionConsolidadaAsesoresAlternoDTO ReporteTasaConversionConsolidadoAsesores(ReporteTasaConversionConsolidadaFiltroDTO filtros);
        List<TCRM_CentroCostoPorAsesorDTO> ObtenerCentroCostoPorAsesor(ReporteTasaConversionConsolidadaFiltroDTO filtros);
        List<DateTime> ObtenerActividadesNoEjecutadas(int idOportunidad);
        List<ReporteSeguimientoOportunidadLogGridDTO> ObtenerOportunidadesLogPorAlumno(int idAlumno, int idOportunidad, int idPadre);
        List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendientePeriodoYCoordinador(ReportePendienteFiltroDTO filtroPendiente);
        List<ReportePendientesCambiosPorCoordinadorDTO> ObtenerReportePendienteCambiosPorCoordinador(ReportePendienteFiltroDTO filtroPendiente);
        List<ReportePendientesDiferenciasDTO> ObtenerReportePendienteDiferencias(ReportePendienteFiltroDTO filtroPendiente);
        List<ReportePendienteDetallesDTO> ObtenerReportePendienteDetalles(ReportePendienteFiltroDTO filtroPendiente);
        List<ReporteSeguimientoOportunidadesOperacionesDTO> ObtenerReporteSeguimientoOportunidadOperaciones(ReporteSeguimientoOportunidadesFiltrosDTO filtro);
        List<ReporteSeguimientoOportunidadesModeloDTO> ObtenerReporteSeguimientoOportunidadProbabilidad(ReporteSeguimientoOportunidadesFiltrosDTO filtro);
        List<ReporteSeguimientoOportunidadDTO> ObtenerReporteSeguimientoOportunidadFC(ReporteSeguimientoOportunidadesFiltrosDTO filtro);
        List<ReporteSeguimientoOportunidadDTO> ObtenerReporteSeguimientoOportunidadFRC(ReporteSeguimientoOportunidadesFiltrosDTO filtro);
        List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO> GenerarReporteEstadoAlumnosPagos(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtro2);
        IEnumerable<ReporteEstadoAlumnosEstadoSubEstadoAgrupadoDTO> GenerarReporteEstadoAlumnoAgrupadoPagos(List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO> respuestaGeneral);
        List<ReporteAvanceAcademicoPresencialOnlineDTO> GenerarReporteEstadoAlumnos2(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtro2);
        IEnumerable<ReporteEstadoAlumnosAgrupadoDTO> GenerarReporteEstadoAlumnoAgrupadoAonline(List<ReporteAvanceAcademicoPresencialOnlineDTO> respuestaGeneral);
        List<ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO> GenerarReporteEstadoAlumnosAvanceAcademicoVSPagos(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtro2);
        IEnumerable<ReporteEstadoAlumnosAvanceAcademicoVSPagosAgrupadoDTO> GenerarReporteEstadoAlumnoAgrupadoAonlineAvanceAcademicoVSPagos(List<ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO> respuestaGeneral);
        List<ReporteAvanceAcademicoAlumnosPagosAtrasados> GenerarReporteEstadoAlumnosPagosAtrasados(string filtro, ReporteTasaConversionConsolidadaFiltroDTO filtro2);
        IEnumerable<ReporteEstadoAlumnosPagosAtrasadosAgrupadoDTO> GenerarReporteEstadoAlumnoAgrupadoAonlineAlumnosPagosAtrasados(List<ReporteAvanceAcademicoAlumnosPagosAtrasados> respuestaGeneral);
        ReporteEstadosAlumnosDTO GenerarReporteEstadoAlumnos(ReporteTasaConversionConsolidadaFiltroDTO filtro);
        public object GenerarReporteDisponibilidadCuota(FiltroReportePagoDTO filtro);
        public ReporteDevolucionesCompuestoDTO ObtenerReporteDevoluciones(ReporteDevolucionesFiltroDTO FiltroDevoluciones);
        public int CongelarReporteDeFlujoPorDia(DateTime FechaCongelamiento, string Usuario);
        public int CongelarReporteDeFlujoPorPeriodo(string Usuario, int IdPeriodo);
        public int CongelarReporteOriginalesPorDia(DateTime FechaCongelamiento, string Usuario);
        public List<ReporteControlDocumentosDTO> ObtenerReporteControlDocumentos(ReporteControlDocumentosFiltroDTO filtroControlDocumentos);
        public List<ReportePagosDTO> ObtenerReportePagos(FiltroFechaDTO filtro);
        public List<ReporteDocumentosPendientesPagoDTO> ObtenerReporteDocumentosPendientesPago();
        ReporteCalidadCambioDeFaseAlternoDTO ReporteCalidadCambioDeFaseAlterno(ReporteCambioFaseFiltrosDTO filtros);
        ReporteContactabilidadAlternoDTO ReporteContactabilidadV2TresCx(ReporteContactabilidadFiltroAlternoDTO filtros);
        List<ReporteContactabilidad3cxAlternoDTO> ReporteContactabilidadV2TresCxAlterno(ReporteContactabilidadFiltroAlternoDTO filtros);
        List<ReporteLlamadaEntranteDTO> ObtenerReporteLlamadaEntrante(FiltroReporteLlamadaEntranteDTO filtros);
    }
}

