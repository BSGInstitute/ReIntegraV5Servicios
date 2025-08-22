using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IReportePendienteMesCoordinadoraRepository
    {
        public List<ReportePendientePeriodoyCoordinadorSeparadoDTO> ObtenerReportePendientePeriodoyCoordinadorPorMesCoordinador(ReportePendienteMesCoordinadorFiltroDTO filtroPendiente);
        public List<ReportePendientePeriodoyCoordinadorSeparadoCierreDTO> ObtenerReportePendienteCierrePorMesCoordinador(ReportePendienteMesCoordinadorFiltroDTO filtroPendiente);
    }
}
