using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISentinelSdtResVenItemRepository : IGenericRepository<TSentinelSdtResVenItem>
    {
        #region Metodos Base
        TSentinelSdtResVenItem Add(SentinelSdtResVenItem entidad);
        TSentinelSdtResVenItem Update(SentinelSdtResVenItem entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSentinelSdtResVenItem> Add(IEnumerable<SentinelSdtResVenItem> listadoEntidad);
        IEnumerable<TSentinelSdtResVenItem> Update(IEnumerable<SentinelSdtResVenItem> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelSdtResVenItemDTO> ObtenerSentinelSdtResVenItem();
        IEnumerable<SentinelSdtResVenItemComboDTO> ObtenerCombo();
        IEnumerable<SentinelSdtResVenItemDTO> ObtenerPorIdSentinel(int idSentinel);
        List<SentinelSdtResVenItemDatosVencidosDTO> ObtenerDatosVencidos(int idSentinel);
    }
}