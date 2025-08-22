using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProveedorCuentaBancoRepository : IGenericRepository<TProveedorCuentaBanco>
    {
        #region Metodos Base
        TProveedorCuentaBanco Add(ProveedorCuentaBanco entidad);
        TProveedorCuentaBanco Update(ProveedorCuentaBanco entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProveedorCuentaBanco> Add(IEnumerable<ProveedorCuentaBanco> listadoEntidad);
        IEnumerable<TProveedorCuentaBanco> Update(IEnumerable<ProveedorCuentaBanco> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProveedorCuentaBancoDTO> ObtenerCuentasProveedorById(int IdProveedor);
    }
}
