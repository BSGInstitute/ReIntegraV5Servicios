using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Detalles asociados a un argumento de programa general.
    /// </summary>
    public partial class TProgramaGeneralArgumentoDetalle
    {
        public TProgramaGeneralArgumentoDetalle()
        {
            TProgramaGeneralArgumentoDetalleMotivacions = new HashSet<TProgramaGeneralArgumentoDetalleMotivacion>();
        }

        /// <summary>
        /// Identificador único de la tabla.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador del argumento de programa general asociado.
        /// </summary>
        public int IdProgramaGeneralArgumento { get; set; }
        /// <summary>
        /// Descripción detallada del argumento.
        /// </summary>
        public string? Detalle { get; set; }
        /// <summary>
        /// Indica si el registro está activo o inactivo.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro por última vez.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora de creación del registro.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la última modificación.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo para control de concurrencia de registros.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TProgramaGeneralArgumento IdProgramaGeneralArgumentoNavigation { get; set; } = null!;
        public virtual ICollection<TProgramaGeneralArgumentoDetalleMotivacion> TProgramaGeneralArgumentoDetalleMotivacions { get; set; }
    }
}
