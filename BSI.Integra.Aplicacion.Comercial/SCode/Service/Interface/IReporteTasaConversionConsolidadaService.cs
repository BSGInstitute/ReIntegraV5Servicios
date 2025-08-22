using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IReporteTasaConversionConsolidadaService
    {
        public TCCReporteDTO GenerarReporte(ReporteTasaConversionConsolidadaFiltroDTO filtros);
        ReporteTasaConversionConsolidadasComboDTO ObtenerCombos(int idPersonal);
    }
}
