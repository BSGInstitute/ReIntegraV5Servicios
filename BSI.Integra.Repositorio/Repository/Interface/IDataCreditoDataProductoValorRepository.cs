using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataProductoValorRepository : IGenericRepository<TDataCreditoDataProductoValor>
    {
        #region Metodos Base
        TDataCreditoDataProductoValor Add(DataCreditoDataProductoValor entidad);
        TDataCreditoDataProductoValor Update(DataCreditoDataProductoValor entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataProductoValor> Add(IEnumerable<DataCreditoDataProductoValor> listadoEntidad);
        IEnumerable<TDataCreditoDataProductoValor> Update(IEnumerable<DataCreditoDataProductoValor> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
