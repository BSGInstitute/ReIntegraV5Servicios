using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IConfiguracionBeneficioProgramaGeneralDatoAdicionalRepository : IGenericRepository<TConfiguracionBeneficioProgramaGeneralDatoAdicional>
    {
        #region Metodos Base
        TConfiguracionBeneficioProgramaGeneralDatoAdicional Add(ConfiguracionBeneficioProgramaGeneralDatoAdicional entidad);
        TConfiguracionBeneficioProgramaGeneralDatoAdicional Update(ConfiguracionBeneficioProgramaGeneralDatoAdicional entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TConfiguracionBeneficioProgramaGeneralDatoAdicional> Add(IEnumerable<ConfiguracionBeneficioProgramaGeneralDatoAdicional> listadoEntidad);
        IEnumerable<TConfiguracionBeneficioProgramaGeneralDatoAdicional> Update(IEnumerable<ConfiguracionBeneficioProgramaGeneralDatoAdicional> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ConfiguracionBeneficioProgramaGeneralDatoAdicional ObtenerPorId(int id);
    }
}
