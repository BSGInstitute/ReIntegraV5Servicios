using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IProveedorCuentaBancoService
    {
        #region Metodos Base
        ProveedorCuentaBanco Add(ProveedorCuentaBanco entidad);
        ProveedorCuentaBanco Update(ProveedorCuentaBanco entidad);
        bool Delete(int id, string usuario);

        List<ProveedorCuentaBanco> Add(List<ProveedorCuentaBanco> listadoEntidad);
        List<ProveedorCuentaBanco> Update(List<ProveedorCuentaBanco> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProveedorCuentaBancoDTO> ObtenerCuentasProveedorById(int IdProveedor);
    }
}
