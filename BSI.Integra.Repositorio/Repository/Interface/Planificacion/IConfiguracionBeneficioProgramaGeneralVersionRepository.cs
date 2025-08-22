using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IConfiguracionBeneficioProgramaGeneralVersionRepository : IGenericRepository<TConfiguracionBeneficioProgramaGeneralVersion>
    {
        #region Metodos Base
        TConfiguracionBeneficioProgramaGeneralVersion Add(ConfiguracionBeneficioProgramaGeneralVersion entidad);
        TConfiguracionBeneficioProgramaGeneralVersion Update(ConfiguracionBeneficioProgramaGeneralVersion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TConfiguracionBeneficioProgramaGeneralVersion> Add(IEnumerable<ConfiguracionBeneficioProgramaGeneralVersion> listadoEntidad);
        IEnumerable<TConfiguracionBeneficioProgramaGeneralVersion> Update(IEnumerable<ConfiguracionBeneficioProgramaGeneralVersion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ConfiguracionBeneficioProgramaGeneralVersion? ObtenerPorId(int id);
        List<ConfiguracionBeneficioProgramaGeneralVersion> ObtenerPorIds(List<int> id);
    }
}
