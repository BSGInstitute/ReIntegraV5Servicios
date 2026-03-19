using System;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    /// <summary>
    /// Resultado del SP pla.SP_GestionDocenteDisparadorPendienteClasificacion.
    /// Contiene los datos necesarios para invocar el servicio externo de IA (Python localhost:8000).
    /// </summary>
    public class DisparadorPendienteClasificacionDTO
    {
        public int       IdDisparadorCongelado { get; set; }
        public string    Canal                 { get; set; }
        public string?   EmailDocente          { get; set; }
        public string?   TelefonoDocente       { get; set; }
        public int       IdPersonal            { get; set; }
        public int       IdGestionContacto     { get; set; }
        public DateTime  FechaEnvio            { get; set; }
        public string?   MensajeEnviado        { get; set; }
        public DateTime? FechaHoraInicio       { get; set; }
    }

    /// <summary>
    /// Respuesta del servicio externo Python (localhost:8000).
    /// Mapea exactamente los campos del response segun doc v3.1.
    /// </summary>
    public class ClasificacionRespuestaResponseDTO
    {
        /// <summary>true si el proceso completo sin errores tecnicos.</summary>
        public bool   Exito                  { get; set; }

        /// <summary>Eco del id_disparador_congelado enviado.</summary>
        public int    IdDisparadorCongelado  { get; set; }

        /// <summary>Canal por el que se buscaron mensajes: WHATSAPP, CORREO.</summary>
        public string Canal                  { get; set; } = string.Empty;

        /// <summary>Cantidad de mensajes del docente encontrados en la ventana.</summary>
        public int    MensajesEncontrados    { get; set; }

        /// <summary>Estado de la actividad en BD: EJECUTADO o POR_EJECUTAR.</summary>
        public string EstadoActualizado      { get; set; } = string.Empty;

        /// <summary>
        /// Campo clave para el polling:
        /// "ESPERANDO"   → plazo no vencido, sin mensajes → volver a llamar en 1 min.
        /// "CLASIFICADO" → resultado definitivo → dejar de llamar.
        /// </summary>
        public string EstadoClasificacion    { get; set; } = string.Empty;

        /// <summary>Detalle de la clasificacion. null cuando EstadoClasificacion = ESPERANDO.</summary>
        public ClasificacionDetalleResponseDTO? Clasificacion { get; set; }
    }

    /// <summary>Detalle del objeto "clasificacion" en la respuesta del Python.</summary>
    public class ClasificacionDetalleResponseDTO
    {
        /// <summary>ID de la ocurrencia congelada clasificada. null si no pudo clasificar.</summary>
        public int?   IdOcurrenciaClasificada { get; set; }

        /// <summary>Nombre de la ocurrencia (ej: ACEPTA, RECHAZA, NO_RESPONDIO).</summary>
        public string NombreOcurrencia        { get; set; } = string.Empty;

        /// <summary>Score de confianza entre 0.0 y 1.0. Solo informativo.</summary>
        public float  ScoreConfianza          { get; set; }

        /// <summary>Nivel de confianza leido de BD: ALTA, MEDIA, BAJA.</summary>
        public string NivelConfianza          { get; set; } = string.Empty;

        /// <summary>
        /// true solo si NivelConfianza = ALTA e IdOcurrenciaClasificada tiene valor.
        /// Si es false el SP NO fue llamado — el asesor debe aprobar manualmente.
        /// </summary>
        public bool   MarcadoAutomatico       { get; set; }

        /// <summary>Justificacion del LLM para la clasificacion.</summary>
        public string Razonamiento            { get; set; } = string.Empty;
    }
}
