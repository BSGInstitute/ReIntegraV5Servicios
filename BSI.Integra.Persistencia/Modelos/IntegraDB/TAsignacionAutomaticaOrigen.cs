using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TAsignacionAutomaticaOrigen
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del origen de asignacion automatica
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Ultima fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public Guid? IdMigracion { get; set; }
    }
}
