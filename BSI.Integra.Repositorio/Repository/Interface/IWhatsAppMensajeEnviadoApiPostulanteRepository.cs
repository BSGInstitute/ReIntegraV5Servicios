using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    /// <summary>
    /// Acceso a datos para el chat WhatsApp del modulo Gestion de Personas (postulante).
    /// Opera sobre 4 SPs en esquema gp:
    ///   - gp.SP_ObtenerMensajesPendientesPostulante (FR-1).
    ///   - gp.SP_ObtenerConversacionesPostulante (FR-2).
    ///   - gp.SP_ObtenerHistorialChatPostulante (FR-3).
    ///   - gp.SP_ValidarMensajesRecibidosEn24HorasPostulante (FR-9: ventana Meta 24h).
    /// Acceso via Dapper a IntegraDB principal.
    /// </summary>
    public interface IWhatsAppMensajeEnviadoApiPostulanteRepository
    {
        Task<IEnumerable<PendienteWhatsAppPostulanteDTO>>    ObtenerPendientesAsync(int idPersonal);
        Task<IEnumerable<ConversacionWhatsAppPostulanteDTO>> ObtenerConversacionesAsync(int idPersonal);

        /// <summary>
        /// FR-3: hilo cronologico crudo del postulante.
        ///
        /// Contrato del valor de retorno (hotfix 404):
        ///   - Devuelve <c>null</c> SI Y SOLO SI el postulante no existe en
        ///     <c>gp.T_Postulante</c> con <c>Estado=1</c> (Id invalido o registro deshabilitado).
        ///     El service mapea ese null a <see cref="Aplicacion.Base.Exceptions.NotFoundException"/> => HTTP 404.
        ///   - Si el postulante existe pero NO tiene mensajes, devuelve un
        ///     <see cref="HistorialChatPostulanteDTO"/> con <c>Mensajes</c> vacio (lista no nula).
        ///     El service responde HTTP 200 con esa lista vacia.
        ///   - Si el postulante existe y tiene mensajes, devuelve el DTO completo.
        /// </summary>
        Task<HistorialChatPostulanteDTO?>                    ObtenerHistorialAsync(int idPostulante, int? idPais);

        /// <summary>
        /// FR-9: valida la ventana de 24h de Meta para texto libre.
        /// Espejo semantico del endpoint ATC `ValidarMesajesEnviadosEn24Horas/{numero}`.
        ///
        /// Retorna:
        ///   true  = ventana CERRADA (no hubo mensaje recibido del postulante en ultimas 24h, requiere plantilla).
        ///   false = ventana ABIERTA (hay mensaje recibido reciente, texto libre permitido).
        /// </summary>
        Task<bool> ValidarVentana24HorasAsync(string numero);
    }
}
