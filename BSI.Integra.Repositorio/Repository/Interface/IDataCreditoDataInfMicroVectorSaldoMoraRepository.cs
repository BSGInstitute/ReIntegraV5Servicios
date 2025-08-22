using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataInfMicroVectorSaldoMoraRepository : IGenericRepository<TDataCreditoDataInfMicroVectorSaldoMora>
    {
        #region Metodos Base
        TDataCreditoDataInfMicroVectorSaldoMora Add(DataCreditoDataInfMicroVectorSaldoMora entidad);
        TDataCreditoDataInfMicroVectorSaldoMora Update(DataCreditoDataInfMicroVectorSaldoMora entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataInfMicroVectorSaldoMora> Add(IEnumerable<DataCreditoDataInfMicroVectorSaldoMora> listadoEntidad);
        IEnumerable<TDataCreditoDataInfMicroVectorSaldoMora> Update(IEnumerable<DataCreditoDataInfMicroVectorSaldoMora> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
