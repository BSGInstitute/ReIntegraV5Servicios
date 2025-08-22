using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IReportePendienteMesCoordinadoraService
    {
        public List<ReportePendientePeriodoyCoordinadorSeparadoCierreDTO> ObtenerReportePendienteCierrePorMesCoordinador(ReportePendienteMesCoordinadorFiltroDTO filtroPendiente);
    }
}
