using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IReporteCambiodeFaseService
    {
        Task<ReporteCambioDeFaseDataDTO> ReporteCambioDeFaseV2IntegraAsync(ReporteCambioFaseFiltrosDTO filtros);
        List<LlamadaObservadaDTO> ObtenerReporteActividadEjecutadaLlamadaObservada(ReporteCambioFaseFiltrosDTO filtros);
        List<AcumuladoTiempoContactoEfectivoDTO> ObtenerAcumuladoTiempoContactoEfectivo(ReporteCambioFaseFiltrosDTO filtros);
        List<LlamadaObservadaDTO> ObtenerAcumuladoLlamadasReprogramadasManualmente(ReporteCambioFaseFiltrosDTO filtros);
        List<ActividadEjecutadaFaseActualDTO> ObtenerActividadEjecutadaFaseActual(ReporteCambioFaseFiltrosDTO filtros);
        Task<ReporteCambioDeFaseDataV2DTO> ReporteCambioDeFaseV2Async(ReporteCambioFaseFiltrosDTO filtros);
        IEnumerable<ReporteCambiosDeFaseOportunidadDTO> ObtenerReporteCambiosDeFaseControlBICYEAcumulado(ReporteCambioFaseFiltrosDTO filtros);
        Task<IEnumerable<ReporteCambiosDeFaseOportunidadDTO>> ObtenerReporteCambiosDeFaseControlBICYEAcumuladoAsync(ReporteCambioFaseFiltrosDTO filtros);
        Task<ReporteCambioDeFaseTasaContactoDTO> GenerarReporteV2TasaContactoAsync(ReporteCambioFaseFiltrosDTO filtros);
        ReporteConteoDatosFaseDTO ObtenerReporteConteoDatosFase(ReporteCambioFaseFiltrosDTO filtros);
        List<ConteoDatosFaseAlternoDTO> ObtenerReporteConteoDatosFaseAlterno(ReporteCambioFaseFiltrosDTO filtros);

    }
}
