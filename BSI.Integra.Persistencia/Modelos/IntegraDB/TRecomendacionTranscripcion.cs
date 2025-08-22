using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena recomendaciones para la trascripcion
    /// </summary>
    public partial class TRecomendacionTranscripcion
    {
        /// <summary>
        /// Llave primaria de la recomendación
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foránea a la transcripción a la que pertenece la recomendación
        /// </summary>
        public int IdTranscripcionLlamada { get; set; }
        /// <summary>
        /// Texto de la recomendación o sugerencia para el asesoramiento
        /// </summary>
        public string Recomendacion { get; set; } = null!;
        /// <summary>
        /// Estado de registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro de recomendación
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro de recomendación
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro de recomendación
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificación del registro de recomendación
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema que almacena la versión del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TTranscripcionLlamadum IdTranscripcionLlamadaNavigation { get; set; } = null!;
    }
}
