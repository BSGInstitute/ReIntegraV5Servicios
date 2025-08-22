using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataInfMicroEvolucionDeudumRepository : IGenericRepository<TDataCreditoDataInfMicroEvolucionDeudum>
    {
        #region Metodos Base
        TDataCreditoDataInfMicroEvolucionDeudum Add(DataCreditoDataInfMicroEvolucionDeudum entidad);
        TDataCreditoDataInfMicroEvolucionDeudum Update(DataCreditoDataInfMicroEvolucionDeudum entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataInfMicroEvolucionDeudum> Add(IEnumerable<DataCreditoDataInfMicroEvolucionDeudum> listadoEntidad);
        IEnumerable<TDataCreditoDataInfMicroEvolucionDeudum> Update(IEnumerable<DataCreditoDataInfMicroEvolucionDeudum> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
