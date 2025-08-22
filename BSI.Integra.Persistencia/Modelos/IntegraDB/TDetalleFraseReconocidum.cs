using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena el detalle de diarizacion de la trascripcion
    /// </summary>
    public partial class TDetalleFraseReconocidum
    {
        /// <summary>
        /// Llave primaria de la tabla nBest
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foránea a la frase reconocida a la que pertenece la opción nBest
        /// </summary>
        public int IdFraseReconocida { get; set; }
        /// <summary>
        /// Confianza asignada a la opción nBest
        /// </summary>
        public decimal? Confidence { get; set; }
        /// <summary>
        /// Contenido textual (lexical) de la opción nBest
        /// </summary>
        public string? Lexical { get; set; }
        /// <summary>
        /// Representación ITN de la opción nBest
        /// </summary>
        public string? Itn { get; set; }
        /// <summary>
        /// Opción nBest con elementos enmascarados
        /// </summary>
        public string? MaskedItn { get; set; }
        /// <summary>
        /// Texto de la opción nBest para presentación
        /// </summary>
        public string? Display { get; set; }
        /// <summary>
        /// Análisis de sentimiento de la opción nBest
        /// </summary>
        public string? Sentiment { get; set; }
        /// <summary>
        /// Estado de registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Fecha de creación del registro de opción nBest
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificación del registro de opción nBest
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Usuario que creó el registro de opción nBest
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro de opción nBest
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo de sistema que almacena la versión del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TFraseReconocidum IdFraseReconocidaNavigation { get; set; } = null!;
    }
}
