using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISentinelSueldoPorIndustriaRepository : IGenericRepository<TSentinelSueldoPorIndustrium>
    {
        #region Metodos Base
        TSentinelSueldoPorIndustrium Add(SentinelSueldoPorIndustria entidad);
        TSentinelSueldoPorIndustrium Update(SentinelSueldoPorIndustria entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSentinelSueldoPorIndustrium> Add(IEnumerable<SentinelSueldoPorIndustria> listadoEntidad);
        IEnumerable<TSentinelSueldoPorIndustrium> Update(IEnumerable<SentinelSueldoPorIndustria> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelSueldoPorIndustriaDTO> ObtenerSentinelSueldoPorIndustria();
        IEnumerable<SentinelSueldoPorIndustriaComboDTO> ObtenerCombo();
        ValorIntDTO ObtenerTipoSueldoIndustria(int idCargo, int idIndustria);
    }
}