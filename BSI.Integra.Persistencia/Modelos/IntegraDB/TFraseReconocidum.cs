using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena la diarizacion de la trascripcion
    /// </summary>
    public partial class TFraseReconocidum
    {
        public TFraseReconocidum()
        {
            TDetalleFraseReconocida = new HashSet<TDetalleFraseReconocidum>();
        }

        /// <summary>
        /// Llave primaria de la frase reconocida
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foránea a la transcripción a la que pertenece la frase reconocida
        /// </summary>
        public int IdTranscripcionLlamada { get; set; }
        /// <summary>
        /// Estado del reconocimiento de la frase (Success, Failure, etc.)
        /// </summary>
        public string? RecognitionStatus { get; set; }
        /// <summary>
        /// Número de canal del audio donde se reconoció la frase
        /// </summary>
        public int? Channel { get; set; }
        /// <summary>
        /// Nombre o identificador del locutor (Ej.: Asesor, Cliente)
        /// </summary>
        public string? Speaker { get; set; }
        /// <summary>
        /// Offset (inicio) de la frase en el audio
        /// </summary>
        public string? Offset { get; set; }
        /// <summary>
        /// Duración de la frase en el audio
        /// </summary>
        public string? Duration { get; set; }
        /// <summary>
        /// Offset en ticks de la frase
        /// </summary>
        public long? OffsetInTicks { get; set; }
        /// <summary>
        /// Duración en ticks de la frase
        /// </summary>
        public long? DurationInTicks { get; set; }
        /// <summary>
        /// Duración de la frase en milisegundos
        /// </summary>
        public int? DurationMilliseconds { get; set; }
        /// <summary>
        /// Offset de la frase en milisegundos
        /// </summary>
        public int? OffsetMilliseconds { get; set; }
        /// <summary>
        /// Estado de registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Fecha de creación del registro de frase reconocida
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificación del registro de frase reconocida
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Usuario que creó el registro de la frase reconocida
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro de la frase reconocida
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo de sistema que almacena la versión del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TTranscripcionLlamadum IdTranscripcionLlamadaNavigation { get; set; } = null!;
        public virtual ICollection<TDetalleFraseReconocidum> TDetalleFraseReconocida { get; set; }
    }
}
