using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IReporteCambioDeFaseTresCxService
    {
        Task<ReporteCambioDeFaseTasaContactoDTO> GenerarReporteTasaContactoTresCxAsync(ReporteCambioFaseFiltrosDTO filtros);
        Task<ReporteCambioDeFaseTasaContactoDTO> GenerarReporteTasaContactoTresCxTotalAsync(ReporteCambioFaseFiltrosDTO filtros);
        Task<ReporteCambioDeFaseTasaContactoWhatsappDTO> GenerarReporteTasaContactoWhatsappAsync(ReporteCambioFaseFiltrosDTO filtros);
        Task<ReporteCambioDeFaseTasaContactoDTO> GenerarReporteTasaContactoTresCxV2Async(ReporteCambioFaseFiltrosDTO filtros);
        Task<ReporteCambioDeFaseTasaContactoDTO> GenerarReporteTasaContactoTresCxOtroMedioAsync(ReporteCambioFaseFiltrosDTO filtros);
        Task<ReporteCambioDeFaseDataV2DTO> ReporteCambioDeFaseV2Async(ReporteCambioFaseFiltrosDTO filtros);
        Task<ReporteCambioDeFaseDataDTO> ReporteCambioDeFaseV2IntegraAsync(ReporteCambioFaseFiltrosDTO filtros);
        List<LlamadaObservadaDTO> ObtenerReporteActividadEjecutadaLlamadaObservada(ReporteCambioFaseFiltrosDTO filtros);
        List<LlamadaObservadaDTO> ObtenerReporteActividadEjecutadaLlamadaObservadaV2(ReporteCambioFaseFiltrosDTO filtros);
        List<AcumuladoTiempoContactoEfectivoDTO> ObtenerAcumuladoTiempoContactoEfectivo(ReporteCambioFaseFiltrosDTO filtros);
        List<LlamadaObservadaDTO> ObtenerAcumuladoLlamadasReprogramadasManualmente(ReporteCambioFaseFiltrosDTO filtros);
        List<ActividadEjecutadaFaseActualDTO> ObtenerActividadEjecutadaFaseActualTresCx(ReporteCambioFaseFiltrosDTO filtros);
        List<ControlOportunidadPredictivaDTO> ObtenerControlOportunidadPredictiva(ReporteCambioFaseFiltrosDTO filtros);
    }
}

