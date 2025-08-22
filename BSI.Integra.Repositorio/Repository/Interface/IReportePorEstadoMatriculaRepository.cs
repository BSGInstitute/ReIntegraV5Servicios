using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IReportePorEstadoMatriculaRepository
    {
        public List<ReporteMatriculadoDTO> ObtenerReporteMatriculados(filtroReportePorEstadoMatriculaDTO filtro);
        public List<ReportePorEstadosMatriculaDTO> ObtenerReportePorEstadosMatricula(filtroReportePorEstadoMatriculaDTO filtro);
    }
}
