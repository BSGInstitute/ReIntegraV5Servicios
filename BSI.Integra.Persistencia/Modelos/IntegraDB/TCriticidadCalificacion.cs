using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena la criticidad de evaluacion de llamada
    /// </summary>
    public partial class TCriticidadCalificacion
    {
        public TCriticidadCalificacion()
        {
            TLineamientoCalificacions = new HashSet<TLineamientoCalificacion>();
        }

        /// <summary>
        /// Identificador único de la criticidad
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la criticidad
        /// </summary>
        public string NombreCriticidad { get; set; } = null!;
        /// <summary>
        /// Descripción de la criticidad
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Estado del registro (activo o inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Versión del registro para control de concurrencia
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TLineamientoCalificacion> TLineamientoCalificacions { get; set; }
    }
}
