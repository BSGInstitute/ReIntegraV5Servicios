using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IArticuloTagService
    {
        #region Metodos Base
        ArticuloTag Add(ArticuloTag entidad);
        ArticuloTag Update(ArticuloTag entidad);
        bool Delete(int id, string usuario);
        List<ArticuloTag> Add(List<ArticuloTag> listadoEntidad);
        List<ArticuloTag> Update(List<ArticuloTag> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        public List<ArticuloTag> ObtenerArticuloTagsAsociados(int IdArticulo);
        public List<FiltroDTO> ObtenerTagsAsociadosArticulo(int IdArticulo);
    }
}
