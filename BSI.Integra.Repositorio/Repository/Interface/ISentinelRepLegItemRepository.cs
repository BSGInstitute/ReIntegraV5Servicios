using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISentinelRepLegItemRepository : IGenericRepository<TSentinelRepLegItem>
    {
        #region Metodos Base
        TSentinelRepLegItem Add(SentinelRepLegItem entidad);
        TSentinelRepLegItem Update(SentinelRepLegItem entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSentinelRepLegItem> Add(IEnumerable<SentinelRepLegItem> listadoEntidad);
        IEnumerable<TSentinelRepLegItem> Update(IEnumerable<SentinelRepLegItem> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelRepLegItemDTO> ObtenerSentinelRepLegItem();
        IEnumerable<SentinelRepLegItemComboDTO> ObtenerCombo();
        IEnumerable<SentinelRepLegItemDTO> ObtenerPorIdSentinel(int idSentinel);
    }
}