using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataNaturalNacionalService
    {
        #region Metodos Base
        DataCreditoDataNaturalNacional Add(DataCreditoDataNaturalNacional entidad);
        DataCreditoDataNaturalNacional Update(DataCreditoDataNaturalNacional entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataNaturalNacional> Add(List<DataCreditoDataNaturalNacional> listadoEntidad);
        List<DataCreditoDataNaturalNacional> Update(List<DataCreditoDataNaturalNacional> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
