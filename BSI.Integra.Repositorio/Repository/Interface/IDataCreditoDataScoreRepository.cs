using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataScoreRepository : IGenericRepository<TDataCreditoDataScore>
    {
        #region Metodos Base
        TDataCreditoDataScore Add(DataCreditoDataScore entidad);
        TDataCreditoDataScore Update(DataCreditoDataScore entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataScore> Add(IEnumerable<DataCreditoDataScore> listadoEntidad);
        IEnumerable<TDataCreditoDataScore> Update(IEnumerable<DataCreditoDataScore> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
