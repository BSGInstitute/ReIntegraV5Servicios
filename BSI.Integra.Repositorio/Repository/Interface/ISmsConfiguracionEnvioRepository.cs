using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISmsConfiguracionEnvioRepository : IGenericRepository<TSmsConfiguracionEnvio>
    {
        #region Metodos Base
        TSmsConfiguracionEnvio Add(SmsConfiguracionEnvio entidad);
        TSmsConfiguracionEnvio Update(SmsConfiguracionEnvio entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSmsConfiguracionEnvio> Add(IEnumerable<SmsConfiguracionEnvio> listadoEntidad);
        IEnumerable<TSmsConfiguracionEnvio> Update(IEnumerable<SmsConfiguracionEnvio> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SmsConfiguracionEnvioDTO> ObtenerSmsConfiguracionEnvio();
        SmsEnvioAnexoDTO ConfiguracionSmsOportunidad(int idOportunidad);
        OportunidadDiasSinContactoDTO ObtenerDiasSinContacto(int idOportunidad);
    }
}