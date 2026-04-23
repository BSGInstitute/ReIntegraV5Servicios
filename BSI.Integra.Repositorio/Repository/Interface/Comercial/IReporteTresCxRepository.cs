using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Comercial
{
    public interface IReporteTresCxRepository
    {
        List<ReporteRealizadaDataTresCxAlternoDTO> ObtenerReporteActividadesRealizadasTresCx(ReporteActividadesRealizadasFiltrosDTO filtro, DateTime fechaInicio, DateTime fechaFin, bool esActual);
        List<ReporteActividadRealizadaDTO> ObtenerReporteActividadesRealizadas(ReporteActividadesRealizadasFiltrosDTO filtro, DateTime fechaInicio, DateTime fechaFin, bool esActual);
        List<ProcesadoDataChatAsistenteVirtualDTO> ReporteChatAsistenteVirtual(ReporteChatAsistenteVirtualFiltroOrdenadoDTO filtro);


    }
}
