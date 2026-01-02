using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catálogo maestro de esquemas para el embudo de remarketing. Cada esquema define un conjunto de niveles de clasificación.
    /// </summary>
    public partial class TRemarketingEmbudoEsquema
    {
        public TRemarketingEmbudoEsquema()
        {
            TRemarketingEmbudoNivels = new HashSet<TRemarketingEmbudoNivel>();
        }

        /// <summary>
        /// Identificador único del esquema de embudo. Clave primaria autoincremental.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre descriptivo del esquema de embudo.
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción detallada del propósito y alcance del esquema de embudo.
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Estado activo/inactivo del esquema.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro del esquema. Normalmente el usuario de sistema o administrador.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que realizó la última modificación al registro del esquema.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora en que se creó el registro del esquema.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la última modificación al registro del esquema.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TRemarketingEmbudoNivel> TRemarketingEmbudoNivels { get; set; }
    }
}
