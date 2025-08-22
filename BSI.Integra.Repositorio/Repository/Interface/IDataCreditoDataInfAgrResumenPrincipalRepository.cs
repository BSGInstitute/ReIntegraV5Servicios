using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataInfAgrResumenPrincipalRepository : IGenericRepository<TDataCreditoDataInfAgrResumenPrincipal>
    {
        #region Metodos Base
        TDataCreditoDataInfAgrResumenPrincipal Add(DataCreditoDataInfAgrResumenPrincipal entidad);
        TDataCreditoDataInfAgrResumenPrincipal Update(DataCreditoDataInfAgrResumenPrincipal entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataInfAgrResumenPrincipal> Add(IEnumerable<DataCreditoDataInfAgrResumenPrincipal> listadoEntidad);
        IEnumerable<TDataCreditoDataInfAgrResumenPrincipal> Update(IEnumerable<DataCreditoDataInfAgrResumenPrincipal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
