using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catálogo de criticidades compartido entre todas las áreas.
    /// </summary>
    public partial class TEvaluacionLlamadaCriticidad
    {
        public TEvaluacionLlamadaCriticidad()
        {
            TEvaluacionLlamadaLineamientos = new HashSet<TEvaluacionLlamadaLineamiento>();
        }

        /// <summary>
        /// Identificador único de la criticidad
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la criticidad
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción de la criticidad
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Estado del registro (1=Activo, 0=Inactivo)
        /// </summary>
        public bool? Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro por última vez
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de última modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Control de versión de fila para concurrencia
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TEvaluacionLlamadaLineamiento> TEvaluacionLlamadaLineamientos { get; set; }
    }
}
