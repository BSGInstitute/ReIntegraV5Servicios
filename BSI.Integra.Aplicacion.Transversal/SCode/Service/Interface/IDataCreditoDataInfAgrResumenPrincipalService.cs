using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataInfAgrResumenPrincipalService
    {
        #region Metodos Base
        DataCreditoDataInfAgrResumenPrincipal Add(DataCreditoDataInfAgrResumenPrincipal entidad);
        DataCreditoDataInfAgrResumenPrincipal Update(DataCreditoDataInfAgrResumenPrincipal entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataInfAgrResumenPrincipal> Add(List<DataCreditoDataInfAgrResumenPrincipal> listadoEntidad);
        List<DataCreditoDataInfAgrResumenPrincipal> Update(List<DataCreditoDataInfAgrResumenPrincipal> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
