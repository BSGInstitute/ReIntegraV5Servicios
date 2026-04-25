using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Asigna cronograma de sesión a actividad detalle
    /// </summary>
    public partial class TGestionContactoActividadDetalleSesion
    {
        /// <summary>
        /// Identificador único de la relación
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteActividadDetalle
        /// </summary>
        public int IdGestionDocenteActividadDetalle { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteSesion
        /// </summary>
        public int IdGestionDocenteSesion { get; set; }
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

        public virtual TGestionDocenteActividadDetalle IdGestionDocenteActividadDetalleNavigation { get; set; } = null!;
        public virtual TGestionDocenteSesion IdGestionDocenteSesionNavigation { get; set; } = null!;
    }
}
