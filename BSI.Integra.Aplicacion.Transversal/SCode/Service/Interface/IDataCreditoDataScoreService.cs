using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataScoreService
    {
        #region Metodos Base
        DataCreditoDataScore Add(DataCreditoDataScore entidad);
        DataCreditoDataScore Update(DataCreditoDataScore entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataScore> Add(List<DataCreditoDataScore> listadoEntidad);
        List<DataCreditoDataScore> Update(List<DataCreditoDataScore> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
