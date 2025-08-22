using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataEndeudamientoGlobalRepository : IGenericRepository<TDataCreditoDataEndeudamientoGlobal>
    {
        #region Metodos Base
        TDataCreditoDataEndeudamientoGlobal Add(DataCreditoDataEndeudamientoGlobal entidad);
        TDataCreditoDataEndeudamientoGlobal Update(DataCreditoDataEndeudamientoGlobal entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataEndeudamientoGlobal> Add(IEnumerable<DataCreditoDataEndeudamientoGlobal> listadoEntidad);
        IEnumerable<TDataCreditoDataEndeudamientoGlobal> Update(IEnumerable<DataCreditoDataEndeudamientoGlobal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
