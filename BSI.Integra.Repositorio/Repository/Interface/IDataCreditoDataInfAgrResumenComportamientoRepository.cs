using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataInfAgrResumenComportamientoRepository : IGenericRepository<TDataCreditoDataInfAgrResumenComportamiento>
    {
        #region Metodos Base
        TDataCreditoDataInfAgrResumenComportamiento Add(DataCreditoDataInfAgrResumenComportamiento entidad);
        TDataCreditoDataInfAgrResumenComportamiento Update(DataCreditoDataInfAgrResumenComportamiento entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataInfAgrResumenComportamiento> Add(IEnumerable<DataCreditoDataInfAgrResumenComportamiento> listadoEntidad);
        IEnumerable<TDataCreditoDataInfAgrResumenComportamiento> Update(IEnumerable<DataCreditoDataInfAgrResumenComportamiento> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
