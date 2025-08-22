using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IReportePresupuestoService
    {
        public IEnumerable<ReportePresupuestoDTO> ObtenerReportePresupuestoFinanzas(FiltroPresupuestoDTO filtros);
        public bool ActualizarEsDiferidoListaFur(DiferirFurDTO datos);
    }
}
