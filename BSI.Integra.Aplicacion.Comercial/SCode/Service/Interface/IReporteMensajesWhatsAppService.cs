using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IReporteMensajesWhatsAppService
    {
        public List<ObtenerReporteMensajesWhatsAppPorTipoDTO> ObtenerReporteMensajesWhatsApp(ReporteMensajesWhatsAppFiltrosDTO filtros);
        public List<ReporteWhatsAppEnvioMasivoDTO> GenerarReporteMensajesMasivosConjuntoLista(ReporteWhatsAppMasivoFiltrosDTO filtros);


    }
}
