using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena Cargos de LinkedIn
    /// </summary>
    public partial class TCargoLinkedIn
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del Cargo
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Estado del Cargo
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario Creacion del Cargo
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario Modificacion del Cargo
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha Creacion del Cargo
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha Modificacion del Cargo
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
