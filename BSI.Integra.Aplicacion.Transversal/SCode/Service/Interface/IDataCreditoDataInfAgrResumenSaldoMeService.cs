using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataInfAgrResumenSaldoMeService
    {
        #region Metodos Base
        DataCreditoDataInfAgrResumenSaldoMe Add(DataCreditoDataInfAgrResumenSaldoMe entidad);
        DataCreditoDataInfAgrResumenSaldoMe Update(DataCreditoDataInfAgrResumenSaldoMe entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataInfAgrResumenSaldoMe> Add(List<DataCreditoDataInfAgrResumenSaldoMe> listadoEntidad);
        List<DataCreditoDataInfAgrResumenSaldoMe> Update(List<DataCreditoDataInfAgrResumenSaldoMe> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
