using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IWhatsAppMensajeRecibidoService
    {
        #region Metodos Base
        WhatsAppMensajeRecibido Add(WhatsAppMensajeRecibido entidad);
        WhatsAppMensajeRecibido Update(WhatsAppMensajeRecibido entidad);
        bool Delete(int id, string usuario);

        List<WhatsAppMensajeRecibido> Add(List<WhatsAppMensajeRecibido> listadoEntidad);
        List<WhatsAppMensajeRecibido> Update(List<WhatsAppMensajeRecibido> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        List<WhatsAppMensajesDTO> ListaUltimoMensajeChatRecibidoControlMensaje(int idPersonal);
        List<WhatsAppMensajesRecibidosOperacionesDTO> ObtenerMensajesRecibidosOperaciones(int idPersonal);
        string GuardarArchivos(byte[] archivo, string tipo, string nombreArchivo);
        List<WhatsAppMensajeRecibido> ObtenerPorWaId(string waId);
        string guardarArchivos(byte[] archivo, string carpetaArchivo, string tipo, string nombreArchivo, int IdPais);
    }
}
