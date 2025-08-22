using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoLogService
    {
        #region Metodos Base
        DataCreditoLog Add(DataCreditoLog entidad);
        DataCreditoLog Update(DataCreditoLog entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoLog> Add(List<DataCreditoLog> listadoEntidad);
        List<DataCreditoLog> Update(List<DataCreditoLog> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
