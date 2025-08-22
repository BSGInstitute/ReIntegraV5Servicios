using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IReportePendienteV2Repository
    {
        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo(ReportePendientePeriodoFiltroPruebaDTO filtroPendiente);
        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendientePeriodoyCoordinadorPorPeriodo_Periodo_Matriculados(ReportePendientePeriodoFiltroPruebaDTO filtroPendiente);
        Task<bool> ObtenerReportePendienteCierrePorPeriodo(ReportePendientePeriodoFiltroPruebaDTO filtroPendiente);
        public List<ReportePendientePeriodoyCoordinadorDTO> ObtenerReportePendienteCierrePorPeriodoPrueba(StringDTO valor);


    }
}
