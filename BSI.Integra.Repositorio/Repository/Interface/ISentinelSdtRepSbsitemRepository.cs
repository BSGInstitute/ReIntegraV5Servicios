using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISentinelSdtRepSbsitemRepository : IGenericRepository<TSentinelSdtRepSbsitem>
    {
        #region Metodos Base
        TSentinelSdtRepSbsitem Add(SentinelSdtRepSbsitem entidad);
        TSentinelSdtRepSbsitem Update(SentinelSdtRepSbsitem entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSentinelSdtRepSbsitem> Add(IEnumerable<SentinelSdtRepSbsitem> listadoEntidad);
        IEnumerable<TSentinelSdtRepSbsitem> Update(IEnumerable<SentinelSdtRepSbsitem> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelSdtRepSbsitemDTO> ObtenerSentinelSdtRepSbsitem();
        IEnumerable<SentinelSdtRepSbsitemComboDTO> ObtenerCombo();
        IEnumerable<SentinelLineaDeudaDatosAlumnoDTO> ObtenerLineaDeudaPorIdSentinel(int idSentinel);
        IEnumerable<SentinelSdtRepSbsitemDTO> ObtenerPorIdSentinel(int idSentinel);
        List<SentinelLineaDeudaDatosAlumnoDTO> ObtenerLineaDeuda(int idSentinel);
        List<SentinelLineaDeudaDatosAlumnoDTO> ObtenerLineaDeudaVigente(int idSentinel);
        List<SentinelLineaDeudaDatosAlumnoDTO> ObtenerLineaDeudaVencida(int idSentinel);
        List<SentinelSdtRepSbsitemLineaDeudaDTO> ObtenerLineaDeudaSentinel(int idSentinel);

    }
}