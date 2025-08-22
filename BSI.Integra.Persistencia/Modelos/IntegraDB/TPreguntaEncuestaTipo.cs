using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena los tipos de preguntas para las encuestas online
    /// </summary>
    public partial class TPreguntaEncuestaTipo
    {
        /// <summary>
        /// Id de tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del Tipo de pregunta
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Estado de registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario creacion de registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario modificacion de registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha creacion de registro
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
