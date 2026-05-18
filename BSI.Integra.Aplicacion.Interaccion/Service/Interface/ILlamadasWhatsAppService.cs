using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Interaccion.Service.Interface
{
    /// Service: ILlamadasWhatsAppService
    /// Autor: WhatsApp Business Calling API integration
    /// Fecha: 2026-05-08
    /// <summary>
    /// Servicio de lectura del historial de llamadas de WhatsApp Business Calling.
    /// </summary>
    public interface ILlamadasWhatsAppService
    {
        LlamadasHistorialResultadoDTO ObtenerHistorial(LlamadasHistorialFiltroDTO filtro);

        /// <summary>
        /// Consulta el estado actual del consentimiento de llamada saliente para un número.
        /// El frontend usa la respuesta para habilitar/deshabilitar el botón "Solicitar llamada"
        /// y decidir si debe pedir consentimiento o si ya puede llamar directo.
        /// </summary>
        LlamadaConsentimientoEstadoDTO ObtenerEstadoConsentimiento(string numeroWhatsApp, int idPais);
    }
}
