using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataCuentaCarteraRepository : IGenericRepository<TDataCreditoDataCuentaCartera>
    {
        #region Metodos Base
        TDataCreditoDataCuentaCartera Add(DataCreditoDataCuentaCartera entidad);
        TDataCreditoDataCuentaCartera Update(DataCreditoDataCuentaCartera entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataCuentaCartera> Add(IEnumerable<DataCreditoDataCuentaCartera> listadoEntidad);
        IEnumerable<TDataCreditoDataCuentaCartera> Update(IEnumerable<DataCreditoDataCuentaCartera> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
