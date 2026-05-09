using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    /// Repositorio: ILlamadasWhatsAppRepository
    /// Autor: WhatsApp Business Calling API integration
    /// Fecha: 2026-05-08
    /// <summary>
    /// Operaciones de lectura para el historial de llamadas de WhatsApp Business Calling.
    /// La escritura (insert/update de llamadas) la maneja WebhookWhatsappApi.
    /// </summary>
    public interface ILlamadasWhatsAppRepository
    {
        /// <summary>
        /// Historial paginado consumiendo com.SP_WhatsappLlamada_ObtenerHistorial.
        /// </summary>
        LlamadasHistorialResultadoDTO ObtenerHistorialPaginado(LlamadasHistorialFiltroDTO filtro);
    }
}
