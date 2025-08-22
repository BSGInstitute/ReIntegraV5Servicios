using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Linq.Expressions;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp.GeneracionDeDataParaConfiguracionPreEnvio;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConfiguracionDeEnvioParaWhatsAppRepository : IGenericRepository<TConfiguracionDeEnvioParaWhatsApp>
    {

        #region Metodos Base
        TConfiguracionDeEnvioParaWhatsApp Add(ConfiguracionDeEnvioParaWhatsApp entidad);
        TConfiguracionDeEnvioParaWhatsApp Update(ConfiguracionDeEnvioParaWhatsApp entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TConfiguracionDeEnvioParaWhatsApp> Add(IEnumerable<ConfiguracionDeEnvioParaWhatsApp> listadoEntidad);
        IEnumerable<TConfiguracionDeEnvioParaWhatsApp> Update(IEnumerable<ConfiguracionDeEnvioParaWhatsApp> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ConfiguracionDeEnvioParaWhatsAppMasCampaniaGeneralYPlantilla ObtenerPrioridadPAraEnvioDeWpp(int id);
        List<ObtenerPrioridadesDeFiltroWppDTO> ObtenerPrioridadesDeFiltroWpp(int idCampaniaGeneral, int IdCampaniaGeneralDetalle);
        List<ObtenerGeneracionDeDataParaConfiguracionPreEnvio> ObtenerDataParaGenerarWhatsAppConfiguracionPreenvio(int idcampaniaGeneral);
    }
}