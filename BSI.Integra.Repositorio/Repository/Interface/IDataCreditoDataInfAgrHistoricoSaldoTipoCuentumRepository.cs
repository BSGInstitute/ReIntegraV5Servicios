using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataInfAgrHistoricoSaldoTipoCuentumRepository : IGenericRepository<TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum>
    {
        #region Metodos Base
        TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum Add(DataCreditoDataInfAgrHistoricoSaldoTipoCuentum entidad);
        TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum Update(DataCreditoDataInfAgrHistoricoSaldoTipoCuentum entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum> Add(IEnumerable<DataCreditoDataInfAgrHistoricoSaldoTipoCuentum> listadoEntidad);
        IEnumerable<TDataCreditoDataInfAgrHistoricoSaldoTipoCuentum> Update(IEnumerable<DataCreditoDataInfAgrHistoricoSaldoTipoCuentum> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
