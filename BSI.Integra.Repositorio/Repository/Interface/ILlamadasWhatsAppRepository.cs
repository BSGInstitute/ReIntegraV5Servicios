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

        /// <summary>
        /// Última solicitud de consentimiento (TipoLlamada=2) para un par (numero, idPais).
        /// Devuelve null si no hay solicitud previa con ConsentimientoEstado seteado.
        /// </summary>
        WhatsAppConsentimientoRawDTO? ObtenerUltimoConsentimiento(string numeroWhatsApp, int idPais);

        /// <summary>
        /// Actualiza GrabacionUrl + GrabacionBlobNombre de una llamada vía SP_WhatsappLlamada_ActualizarGrabacion.
        /// Usado cuando el frontend termina de grabar y sube el blob a Azure.
        /// </summary>
        bool ActualizarGrabacion(int idWhatsappLlamada, string grabacionUrl, string grabacionBlobNombre, string usuarioModificacion);
    }
}
