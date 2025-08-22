using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IReportePagosPorPeriodoService
    {
        public List<PagosIngresosDTO> ObtenerReportePagosIngresos(ReportePagosPorPeriodoFiltroDTO filtro);
        public int CongelarReporteDePagosPorDia(CongelarFlujoDTO filtro, string Usuario);
        public int CongelarReporteDePagosPorPeriodo(CongelarFlujoDTO filtro, string Usuario);
    }
}
