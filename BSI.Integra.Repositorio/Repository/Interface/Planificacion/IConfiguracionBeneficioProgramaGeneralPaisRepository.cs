using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IConfiguracionBeneficioProgramaGeneralPaisRepository : IGenericRepository<TConfiguracionBeneficioProgramaGeneralPai>
    {
        #region Metodos Base
        TConfiguracionBeneficioProgramaGeneralPai Add(ConfiguracionBeneficioProgramaGeneralPais entidad);
        TConfiguracionBeneficioProgramaGeneralPai Update(ConfiguracionBeneficioProgramaGeneralPais entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TConfiguracionBeneficioProgramaGeneralPai> Add(IEnumerable<ConfiguracionBeneficioProgramaGeneralPais> listadoEntidad);
        IEnumerable<TConfiguracionBeneficioProgramaGeneralPai> Update(IEnumerable<ConfiguracionBeneficioProgramaGeneralPais> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ConfiguracionBeneficioProgramaGeneralPais? ObtenerPorId(int id);
        List<ConfiguracionBeneficioProgramaGeneralPais> ObtenerPorIdConfiguracionBeneficioPGneral(int idConfiguracionBeneficioPGneral);
    }
}
