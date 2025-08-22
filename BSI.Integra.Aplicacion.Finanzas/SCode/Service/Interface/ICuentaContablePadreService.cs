using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ICuentaContablePadreService
    {
        #region Metodos Base
        CuentaContablePadre Add(CuentasContablePadreDTO entidad, string Usuario);
        CuentaContablePadre Update(CuentasContablePadreDTO entidad, string Usuario);
        bool Delete(int id, string usuario);

        List<CuentaContablePadre> Add(List<CuentaContablePadre> listadoEntidad);
        List<CuentaContablePadre> Update(List<CuentaContablePadre> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<CuentaContablePadreDTO> ObtenerCuentaContablePadre();
        IEnumerable<CuentaContablePadreComboDTO> ObtenerCombo();
    }
}
