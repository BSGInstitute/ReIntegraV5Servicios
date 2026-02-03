using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteFlujoRepository : IGenericRepository<TGestionDocenteFlujo>
    {
        TGestionDocenteFlujo Add(GestionDocenteFlujo entidad);
        TGestionDocenteFlujo Update(GestionDocenteFlujo entidad);
    }
}
