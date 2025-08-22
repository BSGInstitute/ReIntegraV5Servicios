using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IMontoPagoService
    {
        StringDTO ObtenerProbabilidadSueldoOportunidad(int idOportunidad, int idPais);
        IEnumerable<MontoPagoVersionDTO> ObtenerVersionMontoPagoPorIdOportunidad(int idOportunidad);
        IEnumerable<MontoPagoVersionBeneficiosDTO> ObtenerVersionMontoPagoBeneficiosPorIdOportunidad(int idOportunidad);
        StringDTO ObtenerTablaHTMLVersionMontoPagoBeneficios(int idOportunidad);
        MontoPagoCompuestoDTO ObtenerMontoPagoContadoPorIdOportunidad(int idOportunidad);
        MontoPagoCompuestoDTO ObtenerMontoPagoPorIdOportunidadParaTabla(int idOportunidad);
        IEnumerable<MontoPagoCronogramaCompuestoDTO> ObtenerMontoPagoPorIdOportunidadV2(int idOportunidad);
        List<MontoPagoModalidadDTO> ObtenerMontosPorId(int idPGeneral);
        MontoPagoDTO ObtenerPorId(int idMontoPago);
        List<MontoPagoEtiquetaDTO> ObtenerVersionesMontoPagoV2(int idOportunidad);
        List<MontoPagoEtiquetaDTO> ObtenerVersionesMontoPago(int idOportunidad);
        MontoPagoCompuestoDTO ObtenerMontoPagoPorIdOportunidadSP(int idOportunidad);
        MontoPagoPaqueteDTO ObtenerPaquete(int id);
        List<PaqueteCentroCostoDTO> ObtenerPaquetesIdCentroCosto(int id);
        PgeneralMontoPagoDetalleDTO ObtenerPgeneralMontoPagoDetalle(int idPrograma, int idCategoria);
        List<PGeneralMontoPagoPanelDTO> ListarProgramaGeneralParaMontoPago();
        bool EliminarMontoPago(int idMontoPago, string usuario);
        Task<PGeneralComboMontoPagoModuloDTO> ObtenerCombosModuloAsync();
    }
}
