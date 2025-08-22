using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IMessengerChatService
    {
        #region Metodos Base
        MessengerChat Add(MessengerChat entidad);
        MessengerChat Update(MessengerChat entidad);
        bool Delete(int id, string usuario);

        List<MessengerChat> Add(List<MessengerChat> listadoEntidad);
        List<MessengerChat> Update(List<MessengerChat> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MessengerChatDTO> ObtenerMessengerChat();
        List<MessengerChatHistorialDTO> ObtenerHistorialMessengerChatPorPersonal(int idPersonal, int idAlumno);
        List<MessengerChatHistorialDTO> ObtenerMessengerChatEnviadoPorPersonal(int idPersonal);
        List<MessengerChatHistorialDTO> ObtenerMessengerChatRecibidoPorPersonal(int idPersonal);


        public bool InsertarMensajeEnviado(MessengerMensajeEnviadoDTO datos);

        public bool InsertarMensajeRecibido(MessengerMensajeRecibidoDTO datos);

        public bool InsertarEventMensaje(EventoMensajeMessengerDTO datos);


    }
}
