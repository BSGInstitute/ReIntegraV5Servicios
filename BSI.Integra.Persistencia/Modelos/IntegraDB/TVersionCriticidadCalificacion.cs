using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Criticidades congeladas por versión de configuración
    /// </summary>
    public partial class TVersionCriticidadCalificacion
    {
        public TVersionCriticidadCalificacion()
        {
            TVersionLineamientoCalificacions = new HashSet<TVersionLineamientoCalificacion>();
        }

        /// <summary>
        /// (PK) Primary Key del registro
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
        /// Nivel asociado
        /// </summary>
        public int? Nivel { get; set; }
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool? Estado { get; set; }
        /// <summary>
        /// Usuario que creó
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificación
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Control de concurrencia
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TVersionLineamientoCalificacion> TVersionLineamientoCalificacions { get; set; }
    }
}
