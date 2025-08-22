using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataInfAgrHistoricoSaldoTotalRepository : IGenericRepository<TDataCreditoDataInfAgrHistoricoSaldoTotal>
    {
        #region Metodos Base
        TDataCreditoDataInfAgrHistoricoSaldoTotal Add(DataCreditoDataInfAgrHistoricoSaldoTotal entidad);
        TDataCreditoDataInfAgrHistoricoSaldoTotal Update(DataCreditoDataInfAgrHistoricoSaldoTotal entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataInfAgrHistoricoSaldoTotal> Add(IEnumerable<DataCreditoDataInfAgrHistoricoSaldoTotal> listadoEntidad);
        IEnumerable<TDataCreditoDataInfAgrHistoricoSaldoTotal> Update(IEnumerable<DataCreditoDataInfAgrHistoricoSaldoTotal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
