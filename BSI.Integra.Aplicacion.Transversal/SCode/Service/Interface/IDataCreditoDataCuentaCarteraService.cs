using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataCuentaCarteraService
    {
        #region Metodos Base
        DataCreditoDataCuentaCartera Add(DataCreditoDataCuentaCartera entidad);
        DataCreditoDataCuentaCartera Update(DataCreditoDataCuentaCartera entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataCuentaCartera> Add(List<DataCreditoDataCuentaCartera> listadoEntidad);
        List<DataCreditoDataCuentaCartera> Update(List<DataCreditoDataCuentaCartera> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
