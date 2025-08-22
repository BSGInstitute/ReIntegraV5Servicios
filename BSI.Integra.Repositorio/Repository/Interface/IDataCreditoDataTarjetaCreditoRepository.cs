using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataTarjetaCreditoRepository : IGenericRepository<TDataCreditoDataTarjetaCredito>
    {
        #region Metodos Base
        TDataCreditoDataTarjetaCredito Add(DataCreditoDataTarjetaCredito entidad);
        TDataCreditoDataTarjetaCredito Update(DataCreditoDataTarjetaCredito entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataTarjetaCredito> Add(IEnumerable<DataCreditoDataTarjetaCredito> listadoEntidad);
        IEnumerable<TDataCreditoDataTarjetaCredito> Update(IEnumerable<DataCreditoDataTarjetaCredito> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
