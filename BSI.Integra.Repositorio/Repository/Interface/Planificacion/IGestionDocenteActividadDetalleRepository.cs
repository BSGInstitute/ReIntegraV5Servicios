using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteActividadDetalleRepository : IGenericRepository<TGestionDocenteActividadDetalle>
    {
        TGestionDocenteActividadDetalle Add(GestionDocenteActividadDetalle entidad);
        TGestionDocenteActividadDetalle Update(GestionDocenteActividadDetalle entidad);
    }
}
