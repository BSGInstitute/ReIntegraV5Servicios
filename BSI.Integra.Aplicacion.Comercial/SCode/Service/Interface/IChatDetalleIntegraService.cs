using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IChatDetalleIntegraService
    {
        #region Metodos Base
        ChatDetalleIntegra Add(ChatDetalleIntegra entidad);
        ChatDetalleIntegra Update(ChatDetalleIntegra entidad);
        bool Delete(int id, string usuario);

        List<ChatDetalleIntegra> Add(List<ChatDetalleIntegra> listadoEntidad);
        List<ChatDetalleIntegra> Update(List<ChatDetalleIntegra> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ChatDetalleIntegraDTO> ObtenerChatDetalleIntegra();
        IEnumerable<ChatDetalleIntegraComboDTO> ObtenerCombo();
        HistorialChatRecibidosDTO ObtenerHistorialChatRecibidos(int idPersonal, int idAlumno);
        List<ChatDetalleIntegra> DetalleChatPorIdInteraccion(int idInteraccion);
        List<ChatDetalleIntegra> ObtenerDetalleChatPorIdInteraccionControlMensajesSoporte(int idAlumno);
        ChatDetalleIntegra ObtenerPorIntegraChatYRemintente(int idInteraccionChatIntegra, string idRemitente);
    }
}
