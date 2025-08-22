using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IArticuloSeoRepository : IGenericRepository<TArticuloSeo>
    {
        #region Metodos Base
        TArticuloSeo Add(ArticuloSeo entidad);
        TArticuloSeo Update(ArticuloSeo entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TArticuloSeo> Add(IEnumerable<ArticuloSeo> listadoEntidad);
        IEnumerable<TArticuloSeo> Update(IEnumerable<ArticuloSeo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ParametroSeoComboDTO> ObtenerCombo();
        List<ParametroSeoContenidoArticuloDTO> ObtenerArticuloSeoParametro(int IdArticulo);
    }
}
