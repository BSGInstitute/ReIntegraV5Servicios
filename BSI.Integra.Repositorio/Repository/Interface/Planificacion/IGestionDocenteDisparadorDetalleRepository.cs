using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteDisparadorDetalleRepository : IGenericRepository<TGestionDocenteDisparadorDetalle>
    {
        TGestionDocenteDisparadorDetalle Add(GestionDocenteDisparadorDetalle entidad);
        TGestionDocenteDisparadorDetalle Update(GestionDocenteDisparadorDetalle entidad);
    }
}
