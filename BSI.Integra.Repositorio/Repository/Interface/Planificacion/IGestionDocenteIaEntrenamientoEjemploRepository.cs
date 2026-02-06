using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteIaEntrenamientoEjemploRepository : IGenericRepository<TGestionDocenteIaEntrenamientoEjemplo>
    {
        TGestionDocenteIaEntrenamientoEjemplo Add(GestionDocenteIaEntrenamientoEjemplo entidad);
        TGestionDocenteIaEntrenamientoEjemplo Update(GestionDocenteIaEntrenamientoEjemplo entidad);
    }
}
