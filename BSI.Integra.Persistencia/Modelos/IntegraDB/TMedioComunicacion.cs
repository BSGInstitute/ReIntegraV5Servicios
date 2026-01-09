using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catálogo de medios de comunicación disponibles para contacto con estudiantes (WhatsApp, Llamada, Correo, etc.)
    /// </summary>
    public partial class TMedioComunicacion
    {
        public TMedioComunicacion()
        {
            TPreferenciaComunicacionAcademicas = new HashSet<TPreferenciaComunicacionAcademica>();
        }

        /// <summary>
        /// Identificador único del medio de comunicación
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del medio de comunicación (ej: WhatsApp, Llamada, Correo)
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Estado del registro (1=Activo, 0=Inactivo)
        /// </summary>
        public bool? Estado { get; set; }
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

        public virtual ICollection<TPreferenciaComunicacionAcademica> TPreferenciaComunicacionAcademicas { get; set; }
    }
}
