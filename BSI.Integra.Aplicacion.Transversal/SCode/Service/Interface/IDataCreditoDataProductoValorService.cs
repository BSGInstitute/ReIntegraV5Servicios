using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataProductoValorService
    {
        #region Metodos Base
        DataCreditoDataProductoValor Add(DataCreditoDataProductoValor entidad);
        DataCreditoDataProductoValor Update(DataCreditoDataProductoValor entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataProductoValor> Add(List<DataCreditoDataProductoValor> listadoEntidad);
        List<DataCreditoDataProductoValor> Update(List<DataCreditoDataProductoValor> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
