using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IWhatsAppEstadoMensajeEnviadoRepository : IGenericRepository<TWhatsAppEstadoMensajeEnviado>
    {
        #region Metodos Base
        TWhatsAppEstadoMensajeEnviado Add(WhatsAppEstadoMensajeEnviado entidad);
        TWhatsAppEstadoMensajeEnviado Update(WhatsAppEstadoMensajeEnviado entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TWhatsAppEstadoMensajeEnviado> Add(IEnumerable<WhatsAppEstadoMensajeEnviado> listadoEntidad);
        IEnumerable<TWhatsAppEstadoMensajeEnviado> Update(IEnumerable<WhatsAppEstadoMensajeEnviado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<WhatsAppEstadoMensajeEnviadoDTO> ObtenerWhatsAppEstadoMensajeEnviado();
        IEnumerable<WhatsAppEstadoMensajeEnviadoComboDTO> ObtenerCombo();
        int VerificadEnvioDuplicadoPorEnvioMasivo(string celularWhatsappEstadoMensajeEnviado);
        public List<MensajeEnviadoErroneoWhatsAppDTO> ObtenerReporteMensajesEnviadosErroneos(FiltroMensajesEnviadosErroneosDTO filtros);

    }
}