using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataInfMicroImagenTendenciaEndeudamientoRepository : IGenericRepository<TDataCreditoDataInfMicroImagenTendenciaEndeudamiento>
    {
        #region Metodos Base
        TDataCreditoDataInfMicroImagenTendenciaEndeudamiento Add(DataCreditoDataInfMicroImagenTendenciaEndeudamiento entidad);
        TDataCreditoDataInfMicroImagenTendenciaEndeudamiento Update(DataCreditoDataInfMicroImagenTendenciaEndeudamiento entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataInfMicroImagenTendenciaEndeudamiento> Add(IEnumerable<DataCreditoDataInfMicroImagenTendenciaEndeudamiento> listadoEntidad);
        IEnumerable<TDataCreditoDataInfMicroImagenTendenciaEndeudamiento> Update(IEnumerable<DataCreditoDataInfMicroImagenTendenciaEndeudamiento> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
