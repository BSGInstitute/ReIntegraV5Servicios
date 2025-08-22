using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataInfAgrResumenEndeudamientoService
    {
        #region Metodos Base
        DataCreditoDataInfAgrResumenEndeudamiento Add(DataCreditoDataInfAgrResumenEndeudamiento entidad);
        DataCreditoDataInfAgrResumenEndeudamiento Update(DataCreditoDataInfAgrResumenEndeudamiento entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataInfAgrResumenEndeudamiento> Add(List<DataCreditoDataInfAgrResumenEndeudamiento> listadoEntidad);
        List<DataCreditoDataInfAgrResumenEndeudamiento> Update(List<DataCreditoDataInfAgrResumenEndeudamiento> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
