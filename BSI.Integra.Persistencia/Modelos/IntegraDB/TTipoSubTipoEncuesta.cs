using System;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena la relacion entre tipo y subtipo de encuesta
    /// </summary>
    public partial class TTipoSubTipoEncuesta
    {
        /// <summary>
        /// Pk de tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK a pla.T_TipoEncuesta
        /// </summary>
        public int IdTipoEncuesta { get; set; }
        /// <summary>
        /// FK a pla.T_SubTipoEncuesta
        /// </summary>
        public int IdSubTipoEncuesta { get; set; }
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
        /// Rowversion de registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
