using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoLogRepository : IGenericRepository<TDataCreditoLog>
    {
        #region Metodos Base
        TDataCreditoLog Add(DataCreditoLog entidad);
        TDataCreditoLog Update(DataCreditoLog entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDataCreditoLog> Add(IEnumerable<DataCreditoLog> listadoEntidad);
        IEnumerable<TDataCreditoLog> Update(IEnumerable<DataCreditoLog> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
