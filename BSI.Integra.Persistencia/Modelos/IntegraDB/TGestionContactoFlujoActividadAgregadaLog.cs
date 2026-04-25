using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Log de cambios en actividades agregadas. Rastrea transiciones de estado.
    /// </summary>
    public partial class TGestionContactoFlujoActividadAgregadaLog
    {
        /// <summary>
        /// Identificador único del registro de log. Clave primaria.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la agregación actividad-flujo. Clave foránea a T_GestionContactoFlujoActividadAgregada.
        /// </summary>
        public int IdGestionContactoFlujoActividadAgregada { get; set; }
        /// <summary>
        /// Identificador del estado anterior. NULLABLE.
        /// </summary>
        public int? IdGestionDocenteEstadoEjecucionAnterior { get; set; }
        /// <summary>
        /// Identificador del estado nuevo.
        /// </summary>
        public int IdGestionDocenteEstadoEjecucionNuevo { get; set; }
        /// <summary>
        /// Indicador de estado activo/inactivo. Campo de auditoría obligatorio.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que registró el cambio. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora en UTC-5 del cambio. Campo de auditoría obligatorio.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora en UTC-5 de la última modificación. Campo de auditoría obligatorio.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Control de concurrencia optimista. Campo de auditoría obligatorio.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TGestionContactoFlujoActividadAgregadum IdGestionContactoFlujoActividadAgregadaNavigation { get; set; } = null!;
        public virtual TGestionDocenteEstadoEjecucion? IdGestionDocenteEstadoEjecucionAnteriorNavigation { get; set; }
        public virtual TGestionDocenteEstadoEjecucion IdGestionDocenteEstadoEjecucionNuevoNavigation { get; set; } = null!;
    }
}
