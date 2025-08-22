using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataInfMicroEndeudamientoActualService
    {
        #region Metodos Base
        DataCreditoDataInfMicroEndeudamientoActual Add(DataCreditoDataInfMicroEndeudamientoActual entidad);
        DataCreditoDataInfMicroEndeudamientoActual Update(DataCreditoDataInfMicroEndeudamientoActual entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataInfMicroEndeudamientoActual> Add(List<DataCreditoDataInfMicroEndeudamientoActual> listadoEntidad);
        List<DataCreditoDataInfMicroEndeudamientoActual> Update(List<DataCreditoDataInfMicroEndeudamientoActual> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
