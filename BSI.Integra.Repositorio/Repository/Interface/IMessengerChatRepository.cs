using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IMessengerChatRepository : IGenericRepository<TMessengerChat>
    {
        #region Metodos Base
        TMessengerChat Add(MessengerChat entidad);
        TMessengerChat Update(MessengerChat entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMessengerChat> Add(IEnumerable<MessengerChat> listadoEntidad);
        IEnumerable<TMessengerChat> Update(IEnumerable<MessengerChat> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MessengerChatDTO> ObtenerMessengerChat();
        List<MessengerChatHistorialDTO> ObtenerHistorialMessengerChatPorPersonal(int idPersonal, int idAlumno);
        List<MessengerChatHistorialDTO> ObtenerMessengerChatEnviadoPorPersonal(int idPersonal);
        List<MessengerChatHistorialDTO> ObtenerMessengerChatRecibidoPorPersonal(int idPersonal);
        public bool InsertarMensajeRecibido(MessengerMensajeRecibidoDTO datos);
        public bool InsertarMensajeEnviado(MessengerMensajeEnviadoDTO datos);
        public bool InsertarEventMensaje(EventoMensajeMessengerDTO datos);

    }
}