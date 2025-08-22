using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataInfAgrResumenSaldoRepository : IGenericRepository<TDataCreditoDataInfAgrResumenSaldo>
    {
        #region Metodos Base
        TDataCreditoDataInfAgrResumenSaldo Add(DataCreditoDataInfAgrResumenSaldo entidad);
        TDataCreditoDataInfAgrResumenSaldo Update(DataCreditoDataInfAgrResumenSaldo entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataInfAgrResumenSaldo> Add(IEnumerable<DataCreditoDataInfAgrResumenSaldo> listadoEntidad);
        IEnumerable<TDataCreditoDataInfAgrResumenSaldo> Update(IEnumerable<DataCreditoDataInfAgrResumenSaldo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
