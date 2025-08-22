using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena las relaciones entre las tablas de encuesta y pregunta
    /// </summary>
    public partial class TPreguntaEncuestaOnline
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
        /// Id de talbal T_EncuestaOnline
        /// </summary>
        public int IdEncuestaOnline { get; set; }
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
        public byte[] RowVersion { get; set; } = null!;
    }
}
