using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ILogProyeccionFurRepository : IGenericRepository<TLogProyeccionFur>
    {
        #region Metodos Base
        TLogProyeccionFur Add(LogProyeccionFur entidad);
        TLogProyeccionFur Update(LogProyeccionFur entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TLogProyeccionFur> Add(IEnumerable<LogProyeccionFur> listadoEntidad);
        IEnumerable<TLogProyeccionFur> Update(IEnumerable<LogProyeccionFur> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
