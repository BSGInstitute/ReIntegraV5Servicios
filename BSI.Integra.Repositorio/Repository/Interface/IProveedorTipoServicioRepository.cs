using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProveedorTipoServicioRepository : IGenericRepository<TProveedorTipoServicio>
    {
        #region Metodos Base
        TProveedorTipoServicio Add(ProveedorTipoServicio entidad);
        TProveedorTipoServicio Update(ProveedorTipoServicio entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProveedorTipoServicio> Add(IEnumerable<ProveedorTipoServicio> listadoEntidad);
        IEnumerable<TProveedorTipoServicio> Update(IEnumerable<ProveedorTipoServicio> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProveedorTipoServicioDTO> ObtenerProveedorTipoServicio(List<int> listaIdProveedor);
        void EliminacionLogicoPorPlantilla(int idProveedor, string usuario, List<int> nuevos);
    }
}
