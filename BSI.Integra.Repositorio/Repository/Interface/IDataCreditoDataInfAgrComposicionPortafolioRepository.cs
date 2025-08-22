using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataInfAgrComposicionPortafolioRepository : IGenericRepository<TDataCreditoDataInfAgrComposicionPortafolio>
    {
        #region Metodos Base
        TDataCreditoDataInfAgrComposicionPortafolio Add(DataCreditoDataInfAgrComposicionPortafolio entidad);
        TDataCreditoDataInfAgrComposicionPortafolio Update(DataCreditoDataInfAgrComposicionPortafolio entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataInfAgrComposicionPortafolio> Add(IEnumerable<DataCreditoDataInfAgrComposicionPortafolio> listadoEntidad);
        IEnumerable<TDataCreditoDataInfAgrComposicionPortafolio> Update(IEnumerable<DataCreditoDataInfAgrComposicionPortafolio> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
