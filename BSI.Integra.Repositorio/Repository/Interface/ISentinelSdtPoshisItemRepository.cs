using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISentinelSdtPoshisItemRepository : IGenericRepository<TSentinelSdtPoshisItem>
    {
        #region Metodos Base
        TSentinelSdtPoshisItem Add(SentinelSdtPoshisItem entidad);
        TSentinelSdtPoshisItem Update(SentinelSdtPoshisItem entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSentinelSdtPoshisItem> Add(IEnumerable<SentinelSdtPoshisItem> listadoEntidad);
        IEnumerable<TSentinelSdtPoshisItem> Update(IEnumerable<SentinelSdtPoshisItem> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelSdtPoshisItemDTO> ObtenerSentinelSdtPoshisItem();
        IEnumerable<SentinelSdtPoshisItemComboDTO> ObtenerCombo();
        IEnumerable<SentinelSdtPoshisItemDTO> ObtenerPorIdSentinel(int idSentinel);
        List<SentinelSdtPoshisItemPosicionHistoriaDTO> ObtenerPosicionHistoria(int idSentinel);
    }
}