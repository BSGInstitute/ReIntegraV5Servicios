using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Messenger;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.Messenger
{
    public interface IMessengerFacebookChatRepository
    {
        List<ResumenMessengerFacebookChatDTO> ObtenerGrillaChats(DateTime? fechaInicio, DateTime? fechaFin, string tipo);
        List<ChatMessengerFacebookDTO> ObtenerHistorialChatPorPSID(ObtenerHistorialChatPorPSIDRequestDTO request);
    }
}
