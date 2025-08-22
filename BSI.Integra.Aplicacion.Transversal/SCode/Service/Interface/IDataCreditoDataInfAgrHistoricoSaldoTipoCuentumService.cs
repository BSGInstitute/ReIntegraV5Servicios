using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataInfAgrHistoricoSaldoTipoCuentumService
    {
        #region Metodos Base
        DataCreditoDataInfAgrHistoricoSaldoTipoCuentum Add(DataCreditoDataInfAgrHistoricoSaldoTipoCuentum entidad);
        DataCreditoDataInfAgrHistoricoSaldoTipoCuentum Update(DataCreditoDataInfAgrHistoricoSaldoTipoCuentum entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataInfAgrHistoricoSaldoTipoCuentum> Add(List<DataCreditoDataInfAgrHistoricoSaldoTipoCuentum> listadoEntidad);
        List<DataCreditoDataInfAgrHistoricoSaldoTipoCuentum> Update(List<DataCreditoDataInfAgrHistoricoSaldoTipoCuentum> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
