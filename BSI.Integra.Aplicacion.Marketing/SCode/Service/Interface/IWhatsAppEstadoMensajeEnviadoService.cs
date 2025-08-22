using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IWhatsAppEstadoMensajeEnviadoService
    {
        #region Metodos Base
        WhatsAppEstadoMensajeEnviado Add(WhatsAppEstadoMensajeEnviado entidad);
        WhatsAppEstadoMensajeEnviado Update(WhatsAppEstadoMensajeEnviado entidad);
        bool Delete(int id, string usuario);

        List<WhatsAppEstadoMensajeEnviado> Add(List<WhatsAppEstadoMensajeEnviado> listadoEntidad);
        List<WhatsAppEstadoMensajeEnviado> Update(List<WhatsAppEstadoMensajeEnviado> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<WhatsAppEstadoMensajeEnviadoDTO> ObtenerWhatsAppEstadoMensajeEnviado();
        IEnumerable<WhatsAppEstadoMensajeEnviadoComboDTO> ObtenerCombo();
    }
}
