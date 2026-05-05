using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsAppMensajeEnviadoApiAtcDTO;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IWhatsAppMensajeEnviadoApiAtcService
    {
        Task<RespuestaMensajeAtcDTO> EnviarMensajeValidacion(WhatsAppEnviarMensajeDTO json, string usuario);
        Task<RespuestaMensajeAtcDTO> FinalizarConversacion(FinalizarConversacionDTO json, string usuario);
    }
}
