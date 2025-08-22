using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISentinelSdtLincreItemRepository : IGenericRepository<TSentinelSdtLincreItem>
    {
        #region Metodos Base
        TSentinelSdtLincreItem Add(SentinelSdtLincreItem entidad);
        TSentinelSdtLincreItem Update(SentinelSdtLincreItem entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSentinelSdtLincreItem> Add(IEnumerable<SentinelSdtLincreItem> listadoEntidad);
        IEnumerable<TSentinelSdtLincreItem> Update(IEnumerable<SentinelSdtLincreItem> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelSdtLincreItemDTO> ObtenerSentinelSdtLincreItem();
        IEnumerable<SentinelSdtLincreItemComboDTO> ObtenerCombo();
        IEnumerable<SentinelLineaCreditoDatosAlumnoDTO> ObtenerLineaCreditoPorIdSentinel(int idSentinel);
        IEnumerable<SentinelSdtLincreItemDTO> ObtenerPorIdSentinel(int idSentinel);
        List<AlumnosSentinelLineasCreditoDTO> ObtenerLineaDeCredito(int idSentinel);
    }
}