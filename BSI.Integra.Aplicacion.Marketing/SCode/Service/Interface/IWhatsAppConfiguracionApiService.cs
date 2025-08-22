using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IWhatsAppConfiguracionApiService
    {
        #region Metodos Base
        WhatsAppConfiguracionApi Add(WhatsAppConfiguracionApi entidad);
        WhatsAppConfiguracionApi Update(WhatsAppConfiguracionApi entidad);
        bool Delete(int id, string usuario);
        #endregion
        List<WhatsAppConfiguracionApiListaGrillaDTO> ObtenerCredencialesUsuarios();
        WhatsAppConfiguracionApi ObtenerPorId(int id);
    }
}
