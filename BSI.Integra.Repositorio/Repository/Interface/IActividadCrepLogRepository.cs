using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IActividadCrepLogRepository : IGenericRepository<TActividadCrepLog>
    {
        #region Metodos Base
        TActividadCrepLog Add(ActividadCrepLog entidad);
        TActividadCrepLog Update(ActividadCrepLog entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TActividadCrepLog> Add(IEnumerable<ActividadCrepLog> listadoEntidad);
        IEnumerable<TActividadCrepLog> Update(IEnumerable<ActividadCrepLog> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

    }
}
