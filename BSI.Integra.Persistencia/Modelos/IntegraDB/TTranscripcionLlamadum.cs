using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena datos generales de la transcripcion llamada
    /// </summary>
    public partial class TTranscripcionLlamadum
    {
        public TTranscripcionLlamadum()
        {
            TFraseCombinada = new HashSet<TFraseCombinadum>();
            TFraseReconocida = new HashSet<TFraseReconocidum>();
            TRecomendacionTranscripcions = new HashSet<TRecomendacionTranscripcion>();
        }

        /// <summary>
        /// Llave primaria de la transcripción
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foránea a la llamada a la que pertenece la transcripción
        /// </summary>
        public int IdLlamadaWebphoneCruceCentralTresCx { get; set; }
        /// <summary>
        /// URL o ruta del audio fuente de la transcripción
        /// </summary>
        public string Source { get; set; } = null!;
        /// <summary>
        /// Marca de tiempo en la que se realizó la transcripción
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Duración en ticks del audio transcripto
        /// </summary>
        public long? DurationInTicks { get; set; }
        /// <summary>
        /// Duración en milisegundos del audio transcripto
        /// </summary>
        public int? DurationMilliseconds { get; set; }
        /// <summary>
        /// Duración en formato ISO8601 (PT#M#S) del audio
        /// </summary>
        public string? Duration { get; set; }
        /// <summary>
        /// Resumen o sumario de la transcripción
        /// </summary>
        public string? Summary { get; set; }
        /// <summary>
        /// Indica si la ocurrencia es consistente (1) o no (0)
        /// </summary>
        public bool? OcurrenciaConsistente { get; set; }
        /// <summary>
        /// Estado de registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro de transcripción
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro de transcripción
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro de transcripción
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificación del registro de transcripción
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema que almacena la versión del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TLlamadaWebphoneCruceCentralTresCx IdLlamadaWebphoneCruceCentralTresCxNavigation { get; set; } = null!;
        public virtual ICollection<TFraseCombinadum> TFraseCombinada { get; set; }
        public virtual ICollection<TFraseReconocidum> TFraseReconocida { get; set; }
        public virtual ICollection<TRecomendacionTranscripcion> TRecomendacionTranscripcions { get; set; }
    }
}
