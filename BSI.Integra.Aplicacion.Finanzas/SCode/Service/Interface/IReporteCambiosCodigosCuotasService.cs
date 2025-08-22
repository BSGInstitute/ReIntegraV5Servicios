using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IReporteCambiosCodigosCuotasService
    {
        public List<ReporteCambiosDTO> ObtenerReporteCambios(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios);
        public List<ReporteCodigosDTO> ObtenerReporteCodigos(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios);
        public List<ReporteCuotasDTO> ObtenerReporteCuotas(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios);
        public List<ReporteCambioProgramaDTO> ObtenerReporteTraslados(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios);

    }
}
