using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catálogo de estados para el flujo de aprobación de solicitudes de tipos de descuento (Pendiente, Aprobado, Rechazado, etc.)
    /// </summary>
    public partial class TTipoDescuentoSolicitudEstado
    {
        public TTipoDescuentoSolicitudEstado()
        {
            TTipoDescuentoSolicituds = new HashSet<TTipoDescuentoSolicitud>();
        }

        /// <summary>
        /// Identificador único del estado de solicitud
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del estado (Pendiente, Aprobado, Rechazado, En Revision)
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción detallada del estado y sus implicaciones en el flujo de aprobación
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Indicador de estado del registro (1 = Activo, 0 = Inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario del sistema que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario del sistema que realizó la última modificación del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la última modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Control de versión para concurrencia optimista
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TTipoDescuentoSolicitud> TTipoDescuentoSolicituds { get; set; }
    }
}
