using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ISentinelSdtInfGenService
    {
        #region Metodos Base
        SentinelSdtInfGen Add(SentinelSdtInfGen entidad);
        SentinelSdtInfGen Update(SentinelSdtInfGen entidad);
        bool Delete(int id, string usuario);

        List<SentinelSdtInfGen> Add(List<SentinelSdtInfGen> listadoEntidad);
        List<SentinelSdtInfGen> Update(List<SentinelSdtInfGen> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelSdtInfGenDTO> ObtenerSentinelSdtInfGen();
        IEnumerable<SentinelSdtInfGenComboDTO> ObtenerCombo();
        IEnumerable<SentinelSdtInfGenDTO> ObtenerPorIdSentinel(int idSentinel);
        List<SentinelSdtInfGenDatosGeneralesDTO> ObtenerDatosGenerales(int idSentinel);
        IEnumerable<SentinelSdtInfGen> MapeoEntidadesDesdeListaDTO(List<SentinelSdtInfGenDTO> items);
    }
}
