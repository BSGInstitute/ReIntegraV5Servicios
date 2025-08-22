using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IReporteComisionMatriculaService
    {
        IEnumerable<DTO.ComboDTO> ObtenerListaSubEstadosParaSeguimientoComisiones();
        IEnumerable<ReporteSeguimientoComisionesDTO> ObtenerDatosReporteSeguimientoComisiones(FiltroReporteSeguimientoComisionesDTO filtro);
    }
}

