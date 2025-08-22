using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataInfAgrHistoricoSaldoTotalService
    {
        #region Metodos Base
        DataCreditoDataInfAgrHistoricoSaldoTotal Add(DataCreditoDataInfAgrHistoricoSaldoTotal entidad);
        DataCreditoDataInfAgrHistoricoSaldoTotal Update(DataCreditoDataInfAgrHistoricoSaldoTotal entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataInfAgrHistoricoSaldoTotal> Add(List<DataCreditoDataInfAgrHistoricoSaldoTotal> listadoEntidad);
        List<DataCreditoDataInfAgrHistoricoSaldoTotal> Update(List<DataCreditoDataInfAgrHistoricoSaldoTotal> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
