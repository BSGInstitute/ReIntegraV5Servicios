using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena la Token de la Api de LinkedIn
    /// </summary>
    public partial class TLinkedInToken
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del Token
        /// </summary>
        public string? Token { get; set; }
        /// <summary>
        /// Descripcion del Token
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// FechaExpiracion del Token
        /// </summary>
        public DateTime FechaExpiracion { get; set; }
        /// <summary>
        /// Estado del Token
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario Creacion del Token
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario Modificacion del Token
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha Creacion del Token
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha Modificacion del Token
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
