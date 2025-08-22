using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena registros de categorias de preguntas para las encuestas online
    /// </summary>
    public partial class TPreguntaEncuestaCategorium
    {
        /// <summary>
        /// Pk de tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de categoria pregunta
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Descripcion de categoria de pregunta
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Estado de registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario creacion de registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario Modificacion de registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha creacion de registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha Modificaion de registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Row version de registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
