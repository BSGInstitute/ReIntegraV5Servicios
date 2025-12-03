using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Relaciona detalles de argumentos de programa general con motivaciones.
    /// </summary>
    public partial class TProgramaGeneralArgumentoDetalleMotivacion
    {
        /// <summary>
        /// Identificador único de la tabla.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador del detalle de argumento de programa general asociado.
        /// </summary>
        public int IdProgramaGeneralArgumentoDetalle { get; set; }
        /// <summary>
        /// Identificador de la motivación de programa general asociada.
        /// </summary>
        public int IdProgramaMotivacion { get; set; }
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

        public virtual TProgramaGeneralArgumentoDetalle IdProgramaGeneralArgumentoDetalleNavigation { get; set; } = null!;
        public virtual TProgramaMotivacion IdProgramaMotivacionNavigation { get; set; } = null!;
    }
}
