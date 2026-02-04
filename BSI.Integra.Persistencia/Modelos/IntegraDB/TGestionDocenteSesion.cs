using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catálogo de sesiones para gestión docente
    /// </summary>
    public partial class TGestionDocenteSesion
    {
        public TGestionDocenteSesion()
        {
            TGestionContactoActividadDetalleSesions = new HashSet<TGestionContactoActividadDetalleSesion>();
        }

        /// <summary>
        /// Identificador único de la sesión
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la sesión
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción de la sesión
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Estado del registro (1=Activo, 0=Inactivo)
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
        /// Versión de fila para control de concurrencia
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TGestionContactoActividadDetalleSesion> TGestionContactoActividadDetalleSesions { get; set; }
    }
}
