using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionContactoActividadDetalleSesionRepository : IGenericRepository<TGestionContactoActividadDetalleSesion>
    {
        TGestionContactoActividadDetalleSesion Add(GestionContactoActividadDetalleSesion entidad);
    }
}
