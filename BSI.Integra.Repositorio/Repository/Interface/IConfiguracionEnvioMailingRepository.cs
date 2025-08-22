using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConfiguracionEnvioMailingRepository : IGenericRepository<TConfiguracionEnvioMailing>
    {
        #region Metodos Base
        TConfiguracionEnvioMailing Add(ConfiguracionEnvioMailing entidad);
        TConfiguracionEnvioMailing Update(ConfiguracionEnvioMailing entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TConfiguracionEnvioMailing> Add(IEnumerable<ConfiguracionEnvioMailing> listadoEntidad);
        IEnumerable<TConfiguracionEnvioMailing> Update(IEnumerable<ConfiguracionEnvioMailing> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<CorreoDTO> ObtenerEnviosMasivos(string email);
        CorreoDTO ObtenerEnvioMasivo(int id);
    }
}
