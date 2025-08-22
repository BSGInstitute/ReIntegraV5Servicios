using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataInfMicroPerfilGeneralService
    {
        #region Metodos Base
        DataCreditoDataInfMicroPerfilGeneral Add(DataCreditoDataInfMicroPerfilGeneral entidad);
        DataCreditoDataInfMicroPerfilGeneral Update(DataCreditoDataInfMicroPerfilGeneral entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataInfMicroPerfilGeneral> Add(List<DataCreditoDataInfMicroPerfilGeneral> listadoEntidad);
        List<DataCreditoDataInfMicroPerfilGeneral> Update(List<DataCreditoDataInfMicroPerfilGeneral> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
