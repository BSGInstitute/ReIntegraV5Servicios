using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ISentinelSueldoPorIndustriaDataDinamicoService
    {
        #region Metodos Base
        SentinelSueldoPorIndustriaDataDinamico Add(SentinelSueldoPorIndustriaDataDinamico entidad);
        SentinelSueldoPorIndustriaDataDinamico Update(SentinelSueldoPorIndustriaDataDinamico entidad);
        bool Delete(int id, string usuario);

        List<SentinelSueldoPorIndustriaDataDinamico> Add(List<SentinelSueldoPorIndustriaDataDinamico> listadoEntidad);
        List<SentinelSueldoPorIndustriaDataDinamico> Update(List<SentinelSueldoPorIndustriaDataDinamico> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelSueldoPorIndustriaDataDinamicoDTO> ObtenerSentinelSueldoPorIndustriaDataDinamico();
        IEnumerable<SentinelSueldoPorIndustriaDataDinamicoComboDTO> ObtenerCombo();
        ValorIntDTO ObtenerValorSueldoIndustria(int idCargo, int idIndustria, int idTamanio);
    }
}
