using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IInteraccionRepository : IGenericRepository<TInteraccion>
    {
        #region Metodos Base
        TInteraccion Add(Interaccion entidad);
        TInteraccion Update(Interaccion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TInteraccion> Add(IEnumerable<Interaccion> listadoEntidad);
        IEnumerable<TInteraccion> Update(IEnumerable<Interaccion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
