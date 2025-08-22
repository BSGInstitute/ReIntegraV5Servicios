using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IWhatsAppEnvioAutomaticoService
    {
        #region Metodos Base

        #endregion
        public bool EnvioCorreoMasivosMarketing(string _Subject, string _Message);
        public RespuestaMensajeHookDTO UrlPost(string UrlBase, string jsonStringResult);
        public bool EjecutarCampaniaGeneralEnvioWhatsApp();
        public Task<bool> EjecutarCampaniaGeneralEnvioWhatsAppAsync();
        public bool EnvioWhatsappChatBot(WhatsAppChatBotDTO datos);
        public ResultadoEjecucionCampaniaDTO EjecutarCampaniaGeneralEnvioWhatsAppBoton();

        public Task<RespuestaMensajeHookDTO> UrlPostAsync(string UrlBase, string jsonStringResult);
        

    }
}
