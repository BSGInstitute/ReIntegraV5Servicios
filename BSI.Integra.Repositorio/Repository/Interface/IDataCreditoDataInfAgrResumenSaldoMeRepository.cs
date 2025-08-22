using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataInfAgrResumenSaldoMeRepository : IGenericRepository<TDataCreditoDataInfAgrResumenSaldoMe>
    {
        #region Metodos Base
        TDataCreditoDataInfAgrResumenSaldoMe Add(DataCreditoDataInfAgrResumenSaldoMe entidad);
        TDataCreditoDataInfAgrResumenSaldoMe Update(DataCreditoDataInfAgrResumenSaldoMe entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataInfAgrResumenSaldoMe> Add(IEnumerable<DataCreditoDataInfAgrResumenSaldoMe> listadoEntidad);
        IEnumerable<TDataCreditoDataInfAgrResumenSaldoMe> Update(IEnumerable<DataCreditoDataInfAgrResumenSaldoMe> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
