using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataInfAgrResumenComportamientoService
    {
        #region Metodos Base
        DataCreditoDataInfAgrResumenComportamiento Add(DataCreditoDataInfAgrResumenComportamiento entidad);
        DataCreditoDataInfAgrResumenComportamiento Update(DataCreditoDataInfAgrResumenComportamiento entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataInfAgrResumenComportamiento> Add(List<DataCreditoDataInfAgrResumenComportamiento> listadoEntidad);
        List<DataCreditoDataInfAgrResumenComportamiento> Update(List<DataCreditoDataInfAgrResumenComportamiento> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
