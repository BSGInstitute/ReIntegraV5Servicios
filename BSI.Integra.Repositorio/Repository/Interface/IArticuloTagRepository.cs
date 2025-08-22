using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IArticuloTagRepository : IGenericRepository<TArticuloTag>
    {
        #region Metodos Base
        TArticuloTag Add(ArticuloTag entidad);
        TArticuloTag Update(ArticuloTag entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TArticuloTag> Add(IEnumerable<ArticuloTag> listadoEntidad);
        IEnumerable<TArticuloTag> Update(IEnumerable<ArticuloTag> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<ArticuloTag> ObtenerArticuloTagsAsociados(int IdArticulo);
        List<FiltroDTO> ObtenerTagsAsociadosArticulo(int IdArticulo);
    }
}
