using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISentinelSueldoPorIndustriaDataDinamicoRepository : IGenericRepository<TSentinelSueldoPorIndustriaDataDinamico>
    {
        #region Metodos Base
        TSentinelSueldoPorIndustriaDataDinamico Add(SentinelSueldoPorIndustriaDataDinamico entidad);
        TSentinelSueldoPorIndustriaDataDinamico Update(SentinelSueldoPorIndustriaDataDinamico entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSentinelSueldoPorIndustriaDataDinamico> Add(IEnumerable<SentinelSueldoPorIndustriaDataDinamico> listadoEntidad);
        IEnumerable<TSentinelSueldoPorIndustriaDataDinamico> Update(IEnumerable<SentinelSueldoPorIndustriaDataDinamico> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelSueldoPorIndustriaDataDinamicoDTO> ObtenerSentinelSueldoPorIndustriaDataDinamico();
        IEnumerable<SentinelSueldoPorIndustriaDataDinamicoComboDTO> ObtenerCombo();
        ValorIntDTO ObtenerValorSueldoIndustria(int idCargo, int idIndustria, int idTamanio);
    }
}