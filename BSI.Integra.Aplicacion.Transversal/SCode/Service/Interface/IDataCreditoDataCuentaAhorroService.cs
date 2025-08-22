using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataCuentaAhorroService
    {
        #region Metodos Base
        DataCreditoDataCuentaAhorro Add(DataCreditoDataCuentaAhorro entidad);
        DataCreditoDataCuentaAhorro Update(DataCreditoDataCuentaAhorro entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataCuentaAhorro> Add(List<DataCreditoDataCuentaAhorro> listadoEntidad);
        List<DataCreditoDataCuentaAhorro> Update(List<DataCreditoDataCuentaAhorro> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
