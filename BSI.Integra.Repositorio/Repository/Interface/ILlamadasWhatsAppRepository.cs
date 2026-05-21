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
        /// idNumeroWhatsApp es OPCIONAL — cuando viene, filtra adicionalmente para que el
        /// consent matchee el número de negocio (phone_number_id) que usará la llamada. Crítico
        /// cuando distintos asesores usan distintos WABA numbers: cada uno necesita su consent.
        /// Devuelve null si no hay solicitud previa con ConsentimientoEstado seteado.
        /// </summary>
        WhatsAppConsentimientoRawDTO? ObtenerUltimoConsentimiento(string numeroWhatsApp, int idPais, string? idNumeroWhatsApp = null);

        /// <summary>
        /// Resuelve el NumeroIndentificador (phone_number_id de Meta) que se usará para llamar
        /// con (idPais, idPersonal). Sigue el mismo fallback que el sender: primero busca
        /// config específica del asesor, después la genérica del área. Devuelve null si no hay.
        /// </summary>
        string? ResolverIdNumeroWhatsApp(int idPais, int idPersonal, int idPersonalAreaTrabajo = 8);

        /// <summary>
        /// Actualiza GrabacionUrl + GrabacionBlobNombre de una llamada vía
        /// SP_WhatsappLlamada_ActualizarGrabacion. Usado cuando el frontend termina de grabar
        /// la llamada (MediaRecorder) y sube el blob a Azure.
        /// </summary>
        bool ActualizarGrabacion(int idWhatsappLlamada, string grabacionUrl, string grabacionBlobNombre, string usuarioModificacion);
    }
}
