using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Messenger;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Messenger;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Messenger;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.Marketing.Messenger
{
    public class MessengerFacebookChatService : IMessengerFacebookChatService
    {
        IMessengerFacebookChatRepository _messengerFacebookChatRepository;

        public MessengerFacebookChatService(IMessengerFacebookChatRepository messengerFacebookChatRepository)
        {
            _messengerFacebookChatRepository = messengerFacebookChatRepository;
        }

        public List<ResumenMessengerFacebookChatDTO> ObtenerGrillaChats(DateTime? fechaInicio, DateTime? fechaFin, string tipo)
        {
            fechaInicio = fechaInicio ?? DateTime.UtcNow.Date;
            fechaFin = fechaFin ?? fechaInicio.Value.AddDays(1);

            return _messengerFacebookChatRepository.ObtenerGrillaChats(fechaInicio, fechaFin, tipo);
        }
    }
}
