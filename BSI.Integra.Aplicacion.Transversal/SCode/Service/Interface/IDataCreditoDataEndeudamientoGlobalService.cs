using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataEndeudamientoGlobalService
    {
        #region Metodos Base
        DataCreditoDataEndeudamientoGlobal Add(DataCreditoDataEndeudamientoGlobal entidad);
        DataCreditoDataEndeudamientoGlobal Update(DataCreditoDataEndeudamientoGlobal entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataEndeudamientoGlobal> Add(List<DataCreditoDataEndeudamientoGlobal> listadoEntidad);
        List<DataCreditoDataEndeudamientoGlobal> Update(List<DataCreditoDataEndeudamientoGlobal> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
