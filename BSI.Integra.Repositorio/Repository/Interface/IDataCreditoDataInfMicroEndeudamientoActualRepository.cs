using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataInfMicroEndeudamientoActualRepository : IGenericRepository<TDataCreditoDataInfMicroEndeudamientoActual>
    {
        #region Metodos Base
        TDataCreditoDataInfMicroEndeudamientoActual Add(DataCreditoDataInfMicroEndeudamientoActual entidad);
        TDataCreditoDataInfMicroEndeudamientoActual Update(DataCreditoDataInfMicroEndeudamientoActual entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataInfMicroEndeudamientoActual> Add(IEnumerable<DataCreditoDataInfMicroEndeudamientoActual> listadoEntidad);
        IEnumerable<TDataCreditoDataInfMicroEndeudamientoActual> Update(IEnumerable<DataCreditoDataInfMicroEndeudamientoActual> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
