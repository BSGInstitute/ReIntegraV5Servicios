using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataInfAgrTotalService
    {
        #region Metodos Base
        DataCreditoDataInfAgrTotal Add(DataCreditoDataInfAgrTotal entidad);
        DataCreditoDataInfAgrTotal Update(DataCreditoDataInfAgrTotal entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataInfAgrTotal> Add(List<DataCreditoDataInfAgrTotal> listadoEntidad);
        List<DataCreditoDataInfAgrTotal> Update(List<DataCreditoDataInfAgrTotal> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
