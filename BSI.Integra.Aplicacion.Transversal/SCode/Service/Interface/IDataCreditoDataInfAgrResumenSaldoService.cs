using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataInfAgrResumenSaldoService
    {
        #region Metodos Base
        DataCreditoDataInfAgrResumenSaldo Add(DataCreditoDataInfAgrResumenSaldo entidad);
        DataCreditoDataInfAgrResumenSaldo Update(DataCreditoDataInfAgrResumenSaldo entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataInfAgrResumenSaldo> Add(List<DataCreditoDataInfAgrResumenSaldo> listadoEntidad);
        List<DataCreditoDataInfAgrResumenSaldo> Update(List<DataCreditoDataInfAgrResumenSaldo> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
