using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ISentinelSdtLincreItemService
    {
        bool Delete(List<int> listadoIds, string usuario);
        public IEnumerable<SentinelLineaCreditoDatosAlumnoDTO> ObtenerLineaCreditoPorIdSentinel(int idSentinel);
        IEnumerable<SentinelSdtLincreItemDTO> ObtenerPorIdSentinel(int idSentinel);
        IEnumerable<SentinelSdtLincreItem> MapeoEntidadesDesdeListaDTO(List<SentinelSdtLincreItemDTO> items);
        List<AlumnosSentinelLineasCreditoDTO> ObtenerLineaDeCredito(int idSentinel);
    }
}
