using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataInfMicroAnalisisVectorService
    {
        #region Metodos Base
        DataCreditoDataInfMicroAnalisisVector Add(DataCreditoDataInfMicroAnalisisVector entidad);
        DataCreditoDataInfMicroAnalisisVector Update(DataCreditoDataInfMicroAnalisisVector entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataInfMicroAnalisisVector> Add(List<DataCreditoDataInfMicroAnalisisVector> listadoEntidad);
        List<DataCreditoDataInfMicroAnalisisVector> Update(List<DataCreditoDataInfMicroAnalisisVector> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
