using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataInfAgrTotalRepository : IGenericRepository<TDataCreditoDataInfAgrTotal>
    {
        #region Metodos Base
        TDataCreditoDataInfAgrTotal Add(DataCreditoDataInfAgrTotal entidad);
        TDataCreditoDataInfAgrTotal Update(DataCreditoDataInfAgrTotal entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataInfAgrTotal> Add(IEnumerable<DataCreditoDataInfAgrTotal> listadoEntidad);
        IEnumerable<TDataCreditoDataInfAgrTotal> Update(IEnumerable<DataCreditoDataInfAgrTotal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
