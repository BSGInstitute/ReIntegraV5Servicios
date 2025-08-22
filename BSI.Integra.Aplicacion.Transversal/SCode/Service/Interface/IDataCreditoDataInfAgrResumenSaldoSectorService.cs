using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataInfAgrResumenSaldoSectorService
    {
        #region Metodos Base
        DataCreditoDataInfAgrResumenSaldoSector Add(DataCreditoDataInfAgrResumenSaldoSector entidad);
        DataCreditoDataInfAgrResumenSaldoSector Update(DataCreditoDataInfAgrResumenSaldoSector entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataInfAgrResumenSaldoSector> Add(List<DataCreditoDataInfAgrResumenSaldoSector> listadoEntidad);
        List<DataCreditoDataInfAgrResumenSaldoSector> Update(List<DataCreditoDataInfAgrResumenSaldoSector> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
