using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProductoPresentacionRepository : IGenericRepository<TProductoPresentacion>
    {
        #region Metodos Base
        TProductoPresentacion Add(ProductoPresentacion entidad);
        TProductoPresentacion Update(ProductoPresentacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProductoPresentacion> Add(IEnumerable<ProductoPresentacion> listadoEntidad);
        IEnumerable<TProductoPresentacion> Update(IEnumerable<ProductoPresentacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
    }
}
