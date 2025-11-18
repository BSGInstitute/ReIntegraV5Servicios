using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Messenger;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Messenger
{
    public interface IMessengerFacebookChatService
    {
        List<ResumenMessengerFacebookChatDTO> ObtenerGrillaChats(DateTime? fechaInicio, DateTime? fechaFin, string tipo);
        List<ChatMessengerFacebookDTO> ObtenerHistorialChatPorPSID(ObtenerHistorialChatPorPSIDRequestDTO request);
        Task<EnviarMensajeResponse> EnviarMensajeTexto(EnviarMensajeTextoRequest request);

    }
}
