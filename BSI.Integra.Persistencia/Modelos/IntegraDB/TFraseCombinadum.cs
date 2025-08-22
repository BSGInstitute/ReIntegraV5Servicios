using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena el consolidado general de trascripcion
    /// </summary>
    public partial class TFraseCombinadum
    {
        /// <summary>
        /// Llave primaria de la frase combinada
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foránea a la transcripción a la que pertenece la frase combinada
        /// </summary>
        public int IdTranscripcionLlamada { get; set; }
        /// <summary>
        /// Número de canal del audio de la frase combinada
        /// </summary>
        public int? Channel { get; set; }
        /// <summary>
        /// Contenido textual (lexical) de la frase combinada
        /// </summary>
        public string? Lexical { get; set; }
        /// <summary>
        /// Representación ITN (Inverse Text Normalization) de la frase combinada
        /// </summary>
        public string? Itn { get; set; }
        /// <summary>
        /// Frase con elementos enmascarados, si corresponde
        /// </summary>
        public string? MaskedItn { get; set; }
        /// <summary>
        /// Texto de la frase para presentación
        /// </summary>
        public string? Display { get; set; }
        /// <summary>
        /// Estado de registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro de la frase combinada
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro de la frase combinada
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro de frase combinada
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificación del registro de frase combinada
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema que almacena la versión del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TTranscripcionLlamadum IdTranscripcionLlamadaNavigation { get; set; } = null!;
    }
}
