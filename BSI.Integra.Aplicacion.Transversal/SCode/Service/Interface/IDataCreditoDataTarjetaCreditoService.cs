using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataTarjetaCreditoService
    {
        #region Metodos Base
        DataCreditoDataTarjetaCredito Add(DataCreditoDataTarjetaCredito entidad);
        DataCreditoDataTarjetaCredito Update(DataCreditoDataTarjetaCredito entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataTarjetaCredito> Add(List<DataCreditoDataTarjetaCredito> listadoEntidad);
        List<DataCreditoDataTarjetaCredito> Update(List<DataCreditoDataTarjetaCredito> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
