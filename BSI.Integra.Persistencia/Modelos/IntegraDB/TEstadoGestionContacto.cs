using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catálogo que refleja el estado actual de los contactos en el proceso de gestión comercial
    /// </summary>
    public partial class TEstadoGestionContacto
    {
        public TEstadoGestionContacto()
        {
            TGestionContactoLogs = new HashSet<TGestionContactoLog>();
            TGestionContactos = new HashSet<TGestionContacto>();
        }

        /// <summary>
        /// Identificador único del estado de gestión de contacto (Llave primaria)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del estado de gestión de contacto (ej: Activo, En Seguimiento, Inactivo, Cerrado)
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción detallada sobre el estado del contacto en el proceso de gestión comercial
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Estado del registro (1: Activo, 0: Eliminado/Inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que realizó la última modificación del registro
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
        /// Campo de sistema automático que guarda la versión del registro para control de concurrencia optimista
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TGestionContactoLog> TGestionContactoLogs { get; set; }
        public virtual ICollection<TGestionContacto> TGestionContactos { get; set; }
    }
}
