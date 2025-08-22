using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataInfAgrComposicionPortafolioService
    {
        #region Metodos Base
        DataCreditoDataInfAgrComposicionPortafolio Add(DataCreditoDataInfAgrComposicionPortafolio entidad);
        DataCreditoDataInfAgrComposicionPortafolio Update(DataCreditoDataInfAgrComposicionPortafolio entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataInfAgrComposicionPortafolio> Add(List<DataCreditoDataInfAgrComposicionPortafolio> listadoEntidad);
        List<DataCreditoDataInfAgrComposicionPortafolio> Update(List<DataCreditoDataInfAgrComposicionPortafolio> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
