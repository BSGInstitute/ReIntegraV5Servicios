using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ISentinelSueldoPorIndustriaDataTotalService
    {
        #region Metodos Base
        SentinelSueldoPorIndustriaDataTotal Add(SentinelSueldoPorIndustriaDataTotal entidad);
        SentinelSueldoPorIndustriaDataTotal Update(SentinelSueldoPorIndustriaDataTotal entidad);
        bool Delete(int id, string usuario);

        List<SentinelSueldoPorIndustriaDataTotal> Add(List<SentinelSueldoPorIndustriaDataTotal> listadoEntidad);
        List<SentinelSueldoPorIndustriaDataTotal> Update(List<SentinelSueldoPorIndustriaDataTotal> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelSueldoPorIndustriaDataTotalDTO> ObtenerSentinelSueldoPorIndustriaDataTotal();
        IEnumerable<SentinelSueldoPorIndustriaDataTotalComboDTO> ObtenerCombo();
        ValorIntDTO ObtenerSueldoPorCargoIndustria(int idCargo, int idIndustria);
    }
}
