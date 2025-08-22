using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ISentinelSdtResVenItemService
    {
        #region Metodos Base
        SentinelSdtResVenItem Add(SentinelSdtResVenItem entidad);
        SentinelSdtResVenItem Update(SentinelSdtResVenItem entidad);
        bool Delete(int id, string usuario);

        List<SentinelSdtResVenItem> Add(List<SentinelSdtResVenItem> listadoEntidad);
        List<SentinelSdtResVenItem> Update(List<SentinelSdtResVenItem> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelSdtResVenItemDTO> ObtenerSentinelSdtResVenItem();
        IEnumerable<SentinelSdtResVenItemComboDTO> ObtenerCombo();
        IEnumerable<SentinelSdtResVenItemDTO> ObtenerPorIdSentinel(int idSentinel);
        IEnumerable<SentinelSdtResVenItem> MapeoEntidadesDesdeListaDTO(List<SentinelSdtResVenItemDTO> items);
        List<SentinelSdtResVenItemDatosVencidosDTO> ObtenerDatosVencidos(int idSentinel);
    }
}
