using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteOcurrenciaRepository : IGenericRepository<TGestionDocenteOcurrencium>
    {
        TGestionDocenteOcurrencium Add(GestionDocenteOcurrencia entidad);
        TGestionDocenteOcurrencium Update(GestionDocenteOcurrencia entidad);
    }
}
