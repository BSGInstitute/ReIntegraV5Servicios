using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataInfMicroImagenTendenciaEndeudamientoService
    {
        #region Metodos Base
        DataCreditoDataInfMicroImagenTendenciaEndeudamiento Add(DataCreditoDataInfMicroImagenTendenciaEndeudamiento entidad);
        DataCreditoDataInfMicroImagenTendenciaEndeudamiento Update(DataCreditoDataInfMicroImagenTendenciaEndeudamiento entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataInfMicroImagenTendenciaEndeudamiento> Add(List<DataCreditoDataInfMicroImagenTendenciaEndeudamiento> listadoEntidad);
        List<DataCreditoDataInfMicroImagenTendenciaEndeudamiento> Update(List<DataCreditoDataInfMicroImagenTendenciaEndeudamiento> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
