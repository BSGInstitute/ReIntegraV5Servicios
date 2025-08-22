using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IReporteActividadesRealizadasTresCxService
    {
        List<ProcesadoDataActividadesRealizadasAlternoTresCxDTO> ReporteActividadesRealizadas(ReporteActividadesRealizadasFiltrosDTO? filtro);
    }
}
