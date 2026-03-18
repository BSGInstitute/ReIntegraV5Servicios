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
    /// Respuesta del servicio externo Python tras clasificar la respuesta del docente.
    /// </summary>
    public class ClasificacionRespuestaResponseDTO
    {
        /// <summary>true = resultado definitivo. false = sin respuesta aun.</summary>
        public bool   Clasificado            { get; set; }

        /// <summary>ID de la ocurrencia congelada clasificada. null si no clasifico.</summary>
        public int?   IdOcurrenciaCongelada  { get; set; }

        /// <summary>Nombre de la ocurrencia (ej: ACEPTA, RECHAZA, NO_RESPONDIO).</summary>
        public string NombreOcurrencia       { get; set; } = string.Empty;

        /// <summary>Nivel de confianza: ALTA, MEDIA, BAJA.</summary>
        public string NivelConfianza         { get; set; } = string.Empty;

        /// <summary>Score entre 0.0 y 1.0. Solo informativo.</summary>
        public float  ScoreConfianza         { get; set; }

        /// <summary>Justificacion del LLM.</summary>
        public string Razonamiento           { get; set; } = string.Empty;
    }
}
