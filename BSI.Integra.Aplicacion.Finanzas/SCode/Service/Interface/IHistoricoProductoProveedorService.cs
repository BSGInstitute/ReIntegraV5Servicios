using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IHistoricoProductoProveedorService
    {
        #region Metodos Base
        HistoricoProductoProveedor Add(HistoricoProductoProveedor entidad);
        HistoricoProductoProveedor Update(ActualizarHistoricoDTO entidad);
        bool Delete(int id, string usuario);

        List<HistoricoProductoProveedor> Add(List<HistoricoProductoProveedor> listadoEntidad);
        List<HistoricoProductoProveedor> Update(List<HistoricoProductoProveedor> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<HistoricoProductoProveedorVersionDTO> ObtenerHistoricoUltimaVersion(int? IdHistoricoPP);
        IEnumerable<DTO.ComboDTO> ObtenerNombreHistoricoAutocomplete();
        DetalleFurDTO ObtenerDetalleFUR(int idProducto, int idProveedor);
        ICollection<ProductoPorProveedorDTO> ObtenerListaProductoPorProveedor(int IdProveedor);
        bool insertarHistoricoProducto(HistoricoProductoProveedorVersionDTO objetoHistorico);
    }
}
