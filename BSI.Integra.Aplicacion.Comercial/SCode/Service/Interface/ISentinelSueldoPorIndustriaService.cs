using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ISentinelSueldoPorIndustriaService
    {
        #region Metodos Base
        SentinelSueldoPorIndustria Add(SentinelSueldoPorIndustria entidad);
        SentinelSueldoPorIndustria Update(SentinelSueldoPorIndustria entidad);
        bool Delete(int id, string usuario);

        List<SentinelSueldoPorIndustria> Add(List<SentinelSueldoPorIndustria> listadoEntidad);
        List<SentinelSueldoPorIndustria> Update(List<SentinelSueldoPorIndustria> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelSueldoPorIndustriaDTO> ObtenerSentinelSueldoPorIndustria();
        IEnumerable<SentinelSueldoPorIndustriaComboDTO> ObtenerCombo();
        ValorIntDTO ObtenerTipoSueldoIndustria(int idCargo, int idIndustria);
    }
}
