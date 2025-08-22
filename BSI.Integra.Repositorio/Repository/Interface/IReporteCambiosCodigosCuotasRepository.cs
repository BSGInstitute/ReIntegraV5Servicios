using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IReporteCambiosCodigosCuotasRepository
    {
        public List<ReporteCambiosDTO> ObtenerReporteCambios(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios);
        public List<ReporteCodigosDTO> ObtenerReporteCodigos(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios);
        public List<ReporteCuotasDTO> ObtenerReporteCuotas(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios);
        public List<ReporteCambioProgramaDTO> ObtenerReporteTraslados(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios);
        public int CongelarReporteDeCambios(DateTime FechaCongelamiento, string Usuario);

    }
}
