using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICuentaContablePadreRepository : IGenericRepository<TCuentaContablePadre>
    {
        #region Metodos Base
        TCuentaContablePadre Add(CuentaContablePadre entidad);
        TCuentaContablePadre Update(CuentaContablePadre entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCuentaContablePadre> Add(IEnumerable<CuentaContablePadre> listadoEntidad);
        IEnumerable<TCuentaContablePadre> Update(IEnumerable<CuentaContablePadre> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<CuentaContablePadreDTO> ObtenerCuentaContablePadre();
        IEnumerable<CuentaContablePadreComboDTO> ObtenerCombo();
    }
}