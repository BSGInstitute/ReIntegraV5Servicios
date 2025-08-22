using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISentinelSdtInfGenRepository : IGenericRepository<TSentinelSdtInfGen>
    {
        #region Metodos Base
        TSentinelSdtInfGen Add(SentinelSdtInfGen entidad);
        TSentinelSdtInfGen Update(SentinelSdtInfGen entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSentinelSdtInfGen> Add(IEnumerable<SentinelSdtInfGen> listadoEntidad);
        IEnumerable<TSentinelSdtInfGen> Update(IEnumerable<SentinelSdtInfGen> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelSdtInfGenDTO> ObtenerSentinelSdtInfGen();
        IEnumerable<SentinelSdtInfGenComboDTO> ObtenerCombo();
        IEnumerable<SentinelSdtInfGenDTO> ObtenerPorIdSentinel(int idSentinel);
        List<SentinelSdtInfGenDatosGeneralesDTO> ObtenerDatosGenerales(int idSentinel);
    }
}