using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface
{
    /// <summary>
    /// Servicio del chat WhatsApp GP (postulantes).
    /// Orquesta las 3 lecturas (pendientes/conversaciones/historial) y el passthrough
    /// de envio al WebHookWhatsApp.
    ///
    /// IdPersonal del asesor lo provee el controller leyendolo del JWT (ITokenManager).
    /// El service queda libre de HttpContext para mantenerse testeable.
    /// </summary>
    public interface IWhatsAppMensajeEnviadoApiPostulanteService
    {
        Task<IEnumerable<PendienteWhatsAppPostulanteDTO>>    ObtenerPendientesAsync(int idPersonalAsesor);
        Task<IEnumerable<ConversacionWhatsAppPostulanteDTO>> ObtenerConversacionesAsync(int idPersonalAsesor);
        Task<HistorialChatPostulanteDTO>                     ObtenerHistorialAsync(int idPostulante, int? idPais);
        Task<EnviarMensajeWhatsAppPostulanteResponse>        EnviarMensajeAsync(EnviarMensajeWhatsAppPostulanteRequest request, int idPersonalAsesor);

        /// <summary>
        /// FR-9: ventana Meta 24h. Espejo del endpoint ATC `ValidarMesajesEnviadosEn24Horas/{numero}`.
        /// Retorna true si el front debe forzar plantilla (sin mensaje recibido reciente),
        /// false si puede mandar texto libre (hay mensaje recibido dentro de la ventana).
        /// </summary>
        Task<bool> ValidarVentana24HorasAsync(string numero);
    }
}
