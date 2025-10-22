using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Contiene los argumentos asociados a un programa general.
    /// </summary>
    public partial class TProgramaGeneralArgumento
    {
        public TProgramaGeneralArgumento()
        {
            TProgramaGeneralArgumentoDetalles = new HashSet<TProgramaGeneralArgumentoDetalle>();
            TProgramaGeneralArgumentoModalidads = new HashSet<TProgramaGeneralArgumentoModalidad>();
        }

        /// <summary>
        /// Identificador único de la tabla.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador del programa general asociado.
        /// </summary>
        public int IdPgeneral { get; set; }
        /// <summary>
        /// Nombre del argumento.
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// Descripción detallada del argumento.
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Indica si el argumento es visible en la agenda.
        /// </summary>
        public bool EsVisibleAgenda { get; set; }
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

        public virtual TPgeneral IdPgeneralNavigation { get; set; } = null!;
        public virtual ICollection<TProgramaGeneralArgumentoDetalle> TProgramaGeneralArgumentoDetalles { get; set; }
        public virtual ICollection<TProgramaGeneralArgumentoModalidad> TProgramaGeneralArgumentoModalidads { get; set; }
    }
}
