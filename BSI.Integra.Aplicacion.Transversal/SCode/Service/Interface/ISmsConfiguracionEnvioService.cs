using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ISmsConfiguracionEnvioService
    {
        #region Metodos Base
        SmsConfiguracionEnvio Add(SmsConfiguracionEnvio entidad);
        SmsConfiguracionEnvio Update(SmsConfiguracionEnvio entidad);
        bool Delete(int id, string usuario);

        List<SmsConfiguracionEnvio> Add(List<SmsConfiguracionEnvio> listadoEntidad);
        List<SmsConfiguracionEnvio> Update(List<SmsConfiguracionEnvio> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<SmsConfiguracionEnvioDTO> ObtenerSmsConfiguracionEnvio();
        SmsEnvioAnexoDTO ConfiguracionSmsOportunidad(int idOportunidad);
        OportunidadDiasSinContactoDTO ObtenerDiasSinContacto(int idOportunidad);
    }
}
