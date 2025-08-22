using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProductoRepository : IGenericRepository<TProducto>
    {
        #region Metodos Base
        TProducto Add(Producto entidad);
        TProducto Update(Producto entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProducto> Add(IEnumerable<Producto> listadoEntidad);
        IEnumerable<TProducto> Update(IEnumerable<Producto> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        Producto? ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        IEnumerable<ProductoDTO> ObtenerProducto();
        IEnumerable<ComboDTO> ObtenerListaProductoMaterialesParaCombo();
        IEnumerable<ComboDTO> ObtenerDetalleHistorio(int idProducto, int idProveedor);
        IEnumerable<ComboDTO> ObtenerProductoAutocomplete(string nombre);
        IEnumerable<ProductoCuentaContableDTO> ObtenerProductoCuentaContable(int idProducto);
    }
}
