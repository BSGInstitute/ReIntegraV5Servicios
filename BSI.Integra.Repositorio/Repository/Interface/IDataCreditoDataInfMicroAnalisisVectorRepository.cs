using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataInfMicroAnalisisVectorRepository : IGenericRepository<TDataCreditoDataInfMicroAnalisisVector>
    {
        #region Metodos Base
        TDataCreditoDataInfMicroAnalisisVector Add(DataCreditoDataInfMicroAnalisisVector entidad);
        TDataCreditoDataInfMicroAnalisisVector Update(DataCreditoDataInfMicroAnalisisVector entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataInfMicroAnalisisVector> Add(IEnumerable<DataCreditoDataInfMicroAnalisisVector> listadoEntidad);
        IEnumerable<TDataCreditoDataInfMicroAnalisisVector> Update(IEnumerable<DataCreditoDataInfMicroAnalisisVector> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
