using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataInfMicroEvolucionDeudumService
    {
        #region Metodos Base
        DataCreditoDataInfMicroEvolucionDeudum Add(DataCreditoDataInfMicroEvolucionDeudum entidad);
        DataCreditoDataInfMicroEvolucionDeudum Update(DataCreditoDataInfMicroEvolucionDeudum entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataInfMicroEvolucionDeudum> Add(List<DataCreditoDataInfMicroEvolucionDeudum> listadoEntidad);
        List<DataCreditoDataInfMicroEvolucionDeudum> Update(List<DataCreditoDataInfMicroEvolucionDeudum> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
