using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ILogRepository : IGenericRepository<TLog>
    {
        #region Metodos Base
        TLog Add(Log entidad);
        TLog Update(Log entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TLog> Add(IEnumerable<Log> listadoEntidad);
        IEnumerable<TLog> Update(IEnumerable<Log> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
