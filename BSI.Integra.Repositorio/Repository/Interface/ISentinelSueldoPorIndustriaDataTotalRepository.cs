using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISentinelSueldoPorIndustriaDataTotalRepository : IGenericRepository<TSentinelSueldoPorIndustriaDataTotal>
    {
        #region Metodos Base
        TSentinelSueldoPorIndustriaDataTotal Add(SentinelSueldoPorIndustriaDataTotal entidad);
        TSentinelSueldoPorIndustriaDataTotal Update(SentinelSueldoPorIndustriaDataTotal entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSentinelSueldoPorIndustriaDataTotal> Add(IEnumerable<SentinelSueldoPorIndustriaDataTotal> listadoEntidad);
        IEnumerable<TSentinelSueldoPorIndustriaDataTotal> Update(IEnumerable<SentinelSueldoPorIndustriaDataTotal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelSueldoPorIndustriaDataTotalDTO> ObtenerSentinelSueldoPorIndustriaDataTotal();
        IEnumerable<SentinelSueldoPorIndustriaDataTotalComboDTO> ObtenerCombo();
        ValorIntDTO ObtenerSueldoPorCargoIndustria(int idCargo, int idIndustria);
    }
}