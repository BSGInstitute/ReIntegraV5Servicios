using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataInfAgrResumenEndeudamientoRepository : IGenericRepository<TDataCreditoDataInfAgrResumenEndeudamiento>
    {
        #region Metodos Base
        TDataCreditoDataInfAgrResumenEndeudamiento Add(DataCreditoDataInfAgrResumenEndeudamiento entidad);
        TDataCreditoDataInfAgrResumenEndeudamiento Update(DataCreditoDataInfAgrResumenEndeudamiento entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataInfAgrResumenEndeudamiento> Add(IEnumerable<DataCreditoDataInfAgrResumenEndeudamiento> listadoEntidad);
        IEnumerable<TDataCreditoDataInfAgrResumenEndeudamiento> Update(IEnumerable<DataCreditoDataInfAgrResumenEndeudamiento> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
