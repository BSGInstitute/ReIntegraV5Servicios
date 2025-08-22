using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TModoFur
    {
        public TModoFur()
        {
            TModoPersonalFurs = new HashSet<TModoPersonalFur>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// El valor que toma el modo FUR
        /// </summary>
        public int ModoFur { get; set; }
        /// <summary>
        /// Descripcion del modo FUR
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Row version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// IdMigracion del registro
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual ICollection<TModoPersonalFur> TModoPersonalFurs { get; set; }
    }
}
