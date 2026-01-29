using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Asocia roles de usuario con niveles de aprobación para tipos de descuento. Define qué roles tienen autoridad para aprobar cada nivel
    /// </summary>
    public partial class TTipoDescuentoNivelAprobacionRol
    {
        /// <summary>
        /// Identificador único autoincremental de la relación rol-nivel de aprobación
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la configuración tipo descuento-nivel de aprobación
        /// </summary>
        public int IdTipoDescuentoNivelAprobacion { get; set; }
        /// <summary>
        /// Identificador del rol de usuario con permisos de aprobación
        /// </summary>
        public int IdUsuarioRol { get; set; }
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

        public virtual TTipoDescuentoNivelAprobacion IdTipoDescuentoNivelAprobacionNavigation { get; set; } = null!;
        public virtual TUsuarioRol IdUsuarioRolNavigation { get; set; } = null!;
    }
}
