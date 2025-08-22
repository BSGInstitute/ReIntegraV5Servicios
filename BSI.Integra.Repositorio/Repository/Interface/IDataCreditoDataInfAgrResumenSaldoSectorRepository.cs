using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataInfAgrResumenSaldoSectorRepository : IGenericRepository<TDataCreditoDataInfAgrResumenSaldoSector>
    {
        #region Metodos Base
        TDataCreditoDataInfAgrResumenSaldoSector Add(DataCreditoDataInfAgrResumenSaldoSector entidad);
        TDataCreditoDataInfAgrResumenSaldoSector Update(DataCreditoDataInfAgrResumenSaldoSector entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataInfAgrResumenSaldoSector> Add(IEnumerable<DataCreditoDataInfAgrResumenSaldoSector> listadoEntidad);
        IEnumerable<TDataCreditoDataInfAgrResumenSaldoSector> Update(IEnumerable<DataCreditoDataInfAgrResumenSaldoSector> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
