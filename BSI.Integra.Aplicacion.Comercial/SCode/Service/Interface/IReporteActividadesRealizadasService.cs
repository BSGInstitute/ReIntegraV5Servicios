
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IReporteActividadesRealizadasService
    {
        List<ProcesadoDataActividadesRealizadasAlternoDTO> ReporteActividadesRealizadas(ReporteActividadesRealizadasFiltrosDTO? filtro);
        Task<FiltroReporteActividadRealizadaAlternoDTO> ObtenerCombo(int idPersonal);
    }
}
