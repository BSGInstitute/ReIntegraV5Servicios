using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IReporteComisionMatriculaRepository
    {

        IEnumerable<ComboDTO> ObtenerListaSubEstadosParaSeguimientoComisiones();
        IEnumerable<ReporteSeguimientoComisionesDTO> ObtenerDatosReporteSeguimientoComisiones(FiltroReporteSeguimientoComisionesDTO filtro);
    }
}
