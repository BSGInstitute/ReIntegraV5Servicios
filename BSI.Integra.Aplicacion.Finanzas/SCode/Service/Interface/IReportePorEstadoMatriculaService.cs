using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IReportePorEstadoMatriculaService
    {
        public List<ReporteMatriculadoDTO> ObtenerReporteMatriculados(filtroReportePorEstadoMatriculaDTO filtro);
        public ReportePorEstadosMatriculaFinalDTO ObtenerReportePorEstadosMatricula(filtroReportePorEstadoMatriculaDTO filtro);
    }
}
