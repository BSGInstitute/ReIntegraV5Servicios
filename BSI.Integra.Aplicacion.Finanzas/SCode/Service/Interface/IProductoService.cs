using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IProductoService
    {
        #region Metodos Base
        Producto Add(ProductoDatosDTO entidad, string Usuario);
        Producto InsertarProducto(Producto entidad);
        Producto Update(ProductoDatosDTO entidad, string Usuario);
        Producto ActualizarProducto(Producto entidad);
        bool Delete(int id, string usuario);

        List<Producto> Add(List<Producto> listadoEntidad);
        List<Producto> Update(List<Producto> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ProductoDTO> ObtenerProducto();
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
