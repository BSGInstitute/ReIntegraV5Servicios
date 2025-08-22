
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IArticuloSeoService
    {
        #region Metodos Base
        ArticuloSeo Add(ArticuloSeo entidad);
        ArticuloSeo Update(ArticuloSeo entidad);
        bool Delete(int id, string usuario);
        List<ArticuloSeo> Add(List<ArticuloSeo> listadoEntidad);
        List<ArticuloSeo> Update(List<ArticuloSeo> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ParametroSeoComboDTO> ObtenerCombo();
        List<ParametroSeoContenidoArticuloDTO> ObtenerArticuloSeoParametro(int IdArticulo);
    }
}
