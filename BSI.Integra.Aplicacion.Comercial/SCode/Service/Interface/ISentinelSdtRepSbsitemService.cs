using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ISentinelSdtRepSbsitemService
    {
        #region Metodos Base
        SentinelSdtRepSbsitem Add(SentinelSdtRepSbsitem entidad);
        SentinelSdtRepSbsitem Update(SentinelSdtRepSbsitem entidad);
        bool Delete(int id, string usuario);

        List<SentinelSdtRepSbsitem> Add(List<SentinelSdtRepSbsitem> listadoEntidad);
        List<SentinelSdtRepSbsitem> Update(List<SentinelSdtRepSbsitem> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelSdtRepSbsitemDTO> ObtenerSentinelSdtRepSbsitem();
        IEnumerable<SentinelSdtRepSbsitemComboDTO> ObtenerCombo();
        IEnumerable<SentinelLineaDeudaDatosAlumnoDTO> ObtenerLineaDeudaPorIdSentinel(int idSentinel);
        IEnumerable<SentinelSdtRepSbsitemDTO> ObtenerPorIdSentinel(int idSentinel);
        IEnumerable<SentinelSdtRepSbsitem> MapeoEntidadesDesdeListaDTO(List<SentinelSdtRepSbsitemDTO> items);
        List<SentinelLineaDeudaDatosAlumnoDTO> ObtenerLineaDeuda(int idSentinel);
        List<SentinelLineaDeudaDatosAlumnoDTO> ObtenerLineaDeudaVigente(int idSentinel);
        List<SentinelLineaDeudaDatosAlumnoDTO> ObtenerLineaDeudaVencida(int idSentinel);
        List<SentinelSdtRepSbsitemLineaDeudaDTO> ObtenerLineaDeudaSentinel(int idSentinel);
    }
}
