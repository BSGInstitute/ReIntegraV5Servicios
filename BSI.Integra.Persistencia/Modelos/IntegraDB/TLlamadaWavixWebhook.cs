using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// LlamadaWavixWebHook almacenará todas las llamadas salientes recibidas a través del WebHook generado de Wavix para obtener el estado
    /// 	de las llamadas en tiempo real
    /// </summary>
    public partial class TLlamadaWavixWebhook
    {
        /// <summary>
        /// Identificador único de la llamada
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// UUID asociado a la llamada
        /// </summary>
        public string? Uuid { get; set; }
        /// <summary>
        /// Persona que atendió la llamada
        /// </summary>
        public string? AnsweredBy { get; set; }
        /// <summary>
        /// Costo de la llamada
        /// </summary>
        public string? Charge { get; set; }
        /// <summary>
        /// Fecha y hora de la llamada
        /// </summary>
        public DateTime? Date { get; set; }
        /// <summary>
        /// Número de destino de la llamada
        /// </summary>
        public string? Destination { get; set; }
        /// <summary>
        /// Resultado final de la llamada
        /// </summary>
        public string? Disposition { get; set; }
        /// <summary>
        /// Duración de la llamada en segundos
        /// </summary>
        public int? Duration { get; set; }
        /// <summary>
        /// Número que realizó la llamada
        /// </summary>
        public string? From { get; set; }
        /// <summary>
        /// Costo por minuto de la llamada
        /// </summary>
        public string? PerMinute { get; set; }
        /// <summary>
        /// URL de la grabación de la llamada
        /// </summary>
        public string? RecordingUrl { get; set; }
        /// <summary>
        /// Troncal SIP utilizada para la llamada
        /// </summary>
        public string? SipTrunk { get; set; }
        /// <summary>
        /// Número al que se realizó la llamada
        /// </summary>
        public string? To { get; set; }
        /// <summary>
        /// Transcripción de la llamada
        /// </summary>
        public string? Transcription { get; set; }
        /// <summary>
        /// Tipo de la llamada (Entrante/Saliente)
        /// </summary>
        public string? Tipo { get; set; }
        /// <summary>
        /// Estado actual de la llamada
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Versión de la fila para control de concurrencia
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
