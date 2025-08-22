using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IWhatsAppMensajeRecibidoRepository
    {
        #region Metodos Base
        TWhatsAppMensajeRecibido Add(WhatsAppMensajeRecibido entidad);
        TWhatsAppMensajeRecibido Update(WhatsAppMensajeRecibido entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TWhatsAppMensajeRecibido> Add(IEnumerable<WhatsAppMensajeRecibido> listadoEntidad);
        IEnumerable<TWhatsAppMensajeRecibido> Update(IEnumerable<WhatsAppMensajeRecibido> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<WhatsAppMensajesDTO> ListaUltimoMensajeChatRecibidoControlMensaje(int idPersonal);
        List<WhatsAppMensajesRecibidosOperacionesDTO> ObtenerMensajesRecibidosOperaciones(int idPersonal);
        List<WhatsAppMensajeRecibido> ObtenerPorWaId(string waId);
    }
}
