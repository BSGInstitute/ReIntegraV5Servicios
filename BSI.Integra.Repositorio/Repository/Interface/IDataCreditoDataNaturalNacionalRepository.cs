using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataNaturalNacionalRepository : IGenericRepository<TDataCreditoDataNaturalNacional>
    {
        #region Metodos Base
        TDataCreditoDataNaturalNacional Add(DataCreditoDataNaturalNacional entidad);
        TDataCreditoDataNaturalNacional Update(DataCreditoDataNaturalNacional entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDataCreditoDataNaturalNacional> Add(IEnumerable<DataCreditoDataNaturalNacional> listadoEntidad);
        IEnumerable<TDataCreditoDataNaturalNacional> Update(IEnumerable<DataCreditoDataNaturalNacional> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
