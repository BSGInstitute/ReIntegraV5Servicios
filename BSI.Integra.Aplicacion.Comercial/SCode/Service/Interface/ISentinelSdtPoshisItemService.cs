using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ISentinelSdtPoshisItemService
    {
        #region Metodos Base
        SentinelSdtPoshisItem Add(SentinelSdtPoshisItem entidad);
        SentinelSdtPoshisItem Update(SentinelSdtPoshisItem entidad);
        bool Delete(int id, string usuario);

        List<SentinelSdtPoshisItem> Add(List<SentinelSdtPoshisItem> listadoEntidad);
        List<SentinelSdtPoshisItem> Update(List<SentinelSdtPoshisItem> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelSdtPoshisItemDTO> ObtenerSentinelSdtPoshisItem();
        IEnumerable<SentinelSdtPoshisItemComboDTO> ObtenerCombo();
        IEnumerable<SentinelSdtPoshisItemDTO> ObtenerPorIdSentinel(int idSentinel);
        IEnumerable<SentinelSdtPoshisItem> MapeoEntidadesDesdeListaDTO(List<SentinelSdtPoshisItemDTO> items);
        List<SentinelSdtPoshisItemPosicionHistoriaDTO> ObtenerPosicionHistoria(int idSentinel);
    }
}
