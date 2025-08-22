using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ISentinelSdtEstandarItemService
    {
        #region Metodos Base
        SentinelSdtEstandarItem Add(SentinelSdtEstandarItem entidad);
        SentinelSdtEstandarItem Update(SentinelSdtEstandarItem entidad);
        bool Delete(int id, string usuario);

        List<SentinelSdtEstandarItem> Add(List<SentinelSdtEstandarItem> listadoEntidad);
        List<SentinelSdtEstandarItem> Update(List<SentinelSdtEstandarItem> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelSdtEstandarItemDTO> ObtenerSentinelSdtEstandarItem();
        IEnumerable<SentinelSdtEstandarItemComboDTO> ObtenerCombo();
        IEnumerable<SentinelSdtEstandarItemDTO> ObtenerPorIdSentinel(int idSentinel);
        IEnumerable<SentinelSdtEstandarItem> MapeoEntidadesDesdeListaDTO(List<SentinelSdtEstandarItemDTO> items);
        List<SentinelSdtEstandarItemDniRucDTO> ObtenerDniRucSentinel(int idSentinel);
    }
}
