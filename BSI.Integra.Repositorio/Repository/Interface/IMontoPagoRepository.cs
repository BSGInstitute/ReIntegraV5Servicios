using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IMontoPagoRepository : IGenericRepository<TMontoPago>
    {
        #region Metodos Base
        TMontoPago Add(MontoPago entidad);
        TMontoPago Update(MontoPago entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMontoPago> Add(IEnumerable<MontoPago> listadoEntidad);
        IEnumerable<TMontoPago> Update(IEnumerable<MontoPago> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MontoPagoDTO> ObtenerMontoPago();
        IEnumerable<MontoPagoComboDTO> ObtenerCombo();
        IEnumerable<MontoPagoOportunidadDTO> ObtenerMontoPagoPorIdOportunidad(int idOportunidad);
        StringDTO ObtenerPaquetePorIdMontoPago(int idMontoPago);
        IEnumerable<MontoPagoBeneficiosDTO> ObtenerBeneficiosAnexo03(int idProgramaGeneral, int idPais);
        StringDTO ObtenerProbabilidadSueldoOportunidad(int idOportunidad, int idPais);
        IEnumerable<MontoPagoVersionDTO> ObtenerVersionMontoPagoPorIdOportunidad(int idOportunidad);
        IEnumerable<MontoPagoVersionBeneficiosDTO> ObtenerVersionMontoPagoBeneficiosPorIdOportunidad(int idOportunidad);
        MontoPagoCompuestoDTO ObtenerMontoPagoContadoPorIdOportunidad(int idOportunidad);
        Task<MontoPagoCompuestoDTO> ObtenerMontoPagoContadoPorIdOportunidadAsync(int idOportunidad);
        MontoPagoCompuestoDTO ObtenerMontoPagoPorIdOportunidadParaTabla(int idOportunidad);
        IEnumerable<MontoPagoCronogramaCompuestoDTO> ObtenerPorIdOportunidadV2(int idOportunidad);
        Task<IEnumerable<MontoPagoCronogramaCompuestoDTO>> ObtenerPorIdOportunidadV2Async(int idOportunidad);
        List<MontoPagoModalidadDTO> ObtenerMontosPorId(int idPGeneral);
        Task<List<MontoPagoModalidadDTO>> ObtenerMontosPorIdAsync(int idPGeneral);
        MontoPago ObtenerPorId(int id);
        List<MontoPagoEtiquetaDTO> ObtenerVersionesMontoPago(int idOportunidad);
        List<MontoPagoEtiquetaDTO> ObtenerVersionesMontoPagoV2(int idOportunidad);
        MontoPagoCompuestoDTO ObtenerMontoPagoPorIdOportunidadSP(int idOportunidad);
        MontoPagoPaqueteDTO ObtenerPaquete(int id);
        List<PaqueteCentroCostoDTO> ObtenerPaquetesIdCentroCosto(int id);
        IEnumerable<MontoPagoDTO> ObtenerPorIdPrograma(int idPGeneral);
    }
}