using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataCuentaAhorroRepository : IGenericRepository<TDataCreditoDataCuentaAhorro>
    {
        #region Metodos Base
        TDataCreditoDataCuentaAhorro Add(DataCreditoDataCuentaAhorro entidad);
        TDataCreditoDataCuentaAhorro Update(DataCreditoDataCuentaAhorro entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataCuentaAhorro> Add(IEnumerable<DataCreditoDataCuentaAhorro> listadoEntidad);
        IEnumerable<TDataCreditoDataCuentaAhorro> Update(IEnumerable<DataCreditoDataCuentaAhorro> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
