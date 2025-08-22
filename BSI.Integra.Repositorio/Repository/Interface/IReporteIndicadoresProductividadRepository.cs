using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IReporteIndicadoresProductividadRepository
    {
        public List<ReporteProductividadVentasHorasTrabajadasDTO> ObtenerReporteProductividadVentasHorasTrabajadas(FiltroFechaDTO filtro);
        public List<ReporteProductividadVentasIndicadoresDTO> ObtenerReporteProductividadVentasIndicadores(FiltroFechaDTO filtro);
    }
}
