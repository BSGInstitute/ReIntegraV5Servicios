using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IHistoricoProductoProveedorRepository : IGenericRepository<THistoricoProductoProveedor>
    {
        #region Metodos Base
        THistoricoProductoProveedor Add(HistoricoProductoProveedor entidad);
        THistoricoProductoProveedor Update(HistoricoProductoProveedor entidad);
        bool Delete(int id, string usuario);

        IEnumerable<THistoricoProductoProveedor> Add(IEnumerable<HistoricoProductoProveedor> listadoEntidad);
        IEnumerable<THistoricoProductoProveedor> Update(IEnumerable<HistoricoProductoProveedor> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<HistoricoProductoProveedorVersionDTO> ObtenerHistoricoUltimaVersion(int? IdHistoricoPP);
        IEnumerable<ComboDTO> ObtenerNombreHistoricoAutocomplete();
        DetalleFurDTO? ObtenerDetalleFUR(int idProducto, int idProveedor);
        ICollection<ProductoPorProveedorDTO> ObtenerListaProductoPorProveedor(int IdProveedor);
        public IEnumerable<ProductoPorProveedorUltimaVersionDTO> ObtenerListaProductoPorProveedorUltimaVersion();
        DetalleHistoricoFurDTO ObtenerDetalleHistoricoProyeccionById(int id);

    }
}
