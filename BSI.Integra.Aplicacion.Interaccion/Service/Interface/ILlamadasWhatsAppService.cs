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
        /// idPersonal es OPCIONAL: cuando viene, el filtro adicional por IdNumeroWhatsApp
        /// (resuelto a partir del idPersonal+idPais) asegura que el consent encontrado sea
        /// válido para el número de negocio que ese asesor va a usar. Sin esto, dos asesores
        /// con distintos WABA numbers compartirían erróneamente el consent.
        /// </summary>
        LlamadaConsentimientoEstadoDTO ObtenerEstadoConsentimiento(string numeroWhatsApp, int idPais, int? idPersonal = null);

        /// <summary>
        /// Lista los consentimientos solicitados por un asesor (todos los estados). Usado por
        /// el panel del header global para que el agente vea sus pendientes/aceptados/rechazados
        /// aunque ya no esté en la ficha de origen.
        /// </summary>
        List<ConsentResumenDTO> ObtenerConsentsPorAsesor(int idPersonal);
    }
}
