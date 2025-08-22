using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataInfMicroVectorSaldoMoraService
    {
        #region Metodos Base
        DataCreditoDataInfMicroVectorSaldoMora Add(DataCreditoDataInfMicroVectorSaldoMora entidad);
        DataCreditoDataInfMicroVectorSaldoMora Update(DataCreditoDataInfMicroVectorSaldoMora entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataInfMicroVectorSaldoMora> Add(List<DataCreditoDataInfMicroVectorSaldoMora> listadoEntidad);
        List<DataCreditoDataInfMicroVectorSaldoMora> Update(List<DataCreditoDataInfMicroVectorSaldoMora> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
