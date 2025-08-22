using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ISentinelRepLegItemService
    {
        #region Metodos Base
        SentinelRepLegItem Add(SentinelRepLegItem entidad);
        SentinelRepLegItem Update(SentinelRepLegItem entidad);
        bool Delete(int id, string usuario);

        List<SentinelRepLegItem> Add(List<SentinelRepLegItem> listadoEntidad);
        List<SentinelRepLegItem> Update(List<SentinelRepLegItem> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelRepLegItemDTO> ObtenerSentinelRepLegItem();
        IEnumerable<SentinelRepLegItemComboDTO> ObtenerCombo();
        IEnumerable<SentinelRepLegItemDTO> ObtenerPorIdSentinel(int idSentinel);
        IEnumerable<SentinelRepLegItem> MapeoEntidadesDesdeListaDTO(List<SentinelRepLegItemDTO> items);
    }
}
