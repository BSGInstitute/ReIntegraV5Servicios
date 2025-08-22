using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISentinelSdtEstandarItemRepository : IGenericRepository<TSentinelSdtEstandarItem>
    {
        #region Metodos Base
        TSentinelSdtEstandarItem Add(SentinelSdtEstandarItem entidad);
        TSentinelSdtEstandarItem Update(SentinelSdtEstandarItem entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSentinelSdtEstandarItem> Add(IEnumerable<SentinelSdtEstandarItem> listadoEntidad);
        IEnumerable<TSentinelSdtEstandarItem> Update(IEnumerable<SentinelSdtEstandarItem> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelSdtEstandarItemDTO> ObtenerSentinelSdtEstandarItem();
        IEnumerable<SentinelSdtEstandarItemComboDTO> ObtenerCombo();
        IEnumerable<SentinelSdtEstandarItemDTO> ObtenerPorIdSentinel(int idSentinel);
        List<SentinelSdtEstandarItemDniRucDTO> ObtenerDniRucSentinel(int idSentinel);
    }
}