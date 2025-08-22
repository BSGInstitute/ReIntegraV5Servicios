using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena las respuestas de las preguntas para las ecnuestas online
    /// </summary>
    public partial class TPreguntaEncuestaRespuestum
    {
        /// <summary>
        /// pk de tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id de la tabla T_PreguntaEncuesta
        /// </summary>
        public int IdPreguntaEncuesta { get; set; }
        /// <summary>
        /// respuesta de peregunta
        /// </summary>
        public string? Respuesta { get; set; }
        /// <summary>
        /// orden de respuesta
        /// </summary>
        public int? Orden { get; set; }
        /// <summary>
        /// puntaje de respuesta
        /// </summary>
        public decimal? Puntaje { get; set; }
        /// <summary>
        /// Estado de registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// usuario creacion de registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// usuario modificacion de registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// FEcha creacion de registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha modificacion de registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// row version de registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
