using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TDominioPbx
    {
        /// <summary>
        /// Id relacional encargado de unir al personal con un domio
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del dominio
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Usuario creador del registro
        /// </summary>
        public string? UsuarioCreacion { get; set; }
        /// <summary>
        /// Usuario modificador del registro
        /// </summary>
        public string? UsuarioModificacion { get; set; }
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime? FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime? FechaModificacion { get; set; }
        /// <summary>
        /// Estado para eliminacion logica
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// RowVersion
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// IdMigracion
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
