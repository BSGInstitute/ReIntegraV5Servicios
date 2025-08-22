using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IReportePresupuestoRepository
    {
        public IEnumerable<ReportePresupuestoDTO> ObtenerReportePresupuestoFinanzas(FiltroPresupuestoDTO filtros);
        public bool ActualizarEsDiferidoListaFur(DiferirFurDTO datos);
    }
}
