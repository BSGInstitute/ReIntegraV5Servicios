using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IArticuloPGeneralRepository : IGenericRepository<TArticuloPgeneral>
    {
        #region Metodos Base
        TArticuloPgeneral Add(ArticuloPGeneral entidad);
        TArticuloPgeneral Update(ArticuloPGeneral entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TArticuloPgeneral> Add(IEnumerable<ArticuloPGeneral> listadoEntidad);
        IEnumerable<TArticuloPgeneral> Update(IEnumerable<ArticuloPGeneral> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<ArticuloPGeneral> ObtenerArticuloPGeneralAsociados(int IdArticulo);
    }
}
