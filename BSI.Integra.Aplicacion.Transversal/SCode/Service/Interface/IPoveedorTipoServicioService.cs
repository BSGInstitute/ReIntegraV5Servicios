using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IProveedorTipoServicioService
    {
        #region Metodos Base
        ProveedorTipoServicio Add(ProveedorTipoServicio entidad);
        ProveedorTipoServicio Update(ProveedorTipoServicio entidad);
        bool Delete(int id, string usuario);

        List<ProveedorTipoServicio> Add(List<ProveedorTipoServicio> listadoEntidad);
        List<ProveedorTipoServicio> Update(List<ProveedorTipoServicio> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ProveedorTipoServicioDTO> ObtenerProveedorTipoServicio(List<int> listaIdProveedor);
        void EliminacionLogicoPorPlantilla(int idProveedor, string usuario, List<int> nuevos);
    }
}
