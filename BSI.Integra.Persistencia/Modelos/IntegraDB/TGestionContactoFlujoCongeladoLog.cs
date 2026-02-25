using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Log de cambios en flujos congelados. Rastrea transiciones de estado de ejecución de flujos congelados.
    /// </summary>
    public partial class TGestionContactoFlujoCongeladoLog
    {
        /// <summary>
        /// Identificador único del registro de log. Clave primaria. Generado automáticamente.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador del flujo congelado. Clave foránea a T_GestionContactoFlujoCongelado. Referencia el registro modificado.
        /// </summary>
        public int IdGestionContactoFlujoCongelado { get; set; }
        /// <summary>
        /// Identificador del estado de ejecución anterior. Referencia a T_GestionDocenteEstadoEjecucion. NULLABLE. Puede ser nulo si es el primer cambio de estado.
        /// </summary>
        public int? IdGestionDocenteEstadoEjecucionAnterior { get; set; }
        /// <summary>
        /// Identificador del nuevo estado de ejecución. Referencia a T_GestionDocenteEstadoEjecucion. Estado al cual cambió la entidad.
        /// </summary>
        public int IdGestionDocenteEstadoEjecucionNuevo { get; set; }
        /// <summary>
        /// Indicador de estado activo/inactivo del registro log. 1 = Activo, 0 = Inactivo. Campo de auditoría obligatorio.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que registró el cambio de estado. Máximo 50 caracteres. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro del log. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora exacta en UTC-5 del cambio de estado. Marca temporal precisa. Campo de auditoría obligatorio.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora en UTC-5 de la última modificación del log. Campo de auditoría obligatorio.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Control de concurrencia optimista. Campo de auditoría obligatorio.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TGestionContactoFlujoCongelado IdGestionContactoFlujoCongeladoNavigation { get; set; } = null!;
        public virtual TGestionDocenteEstadoEjecucion? IdGestionDocenteEstadoEjecucionAnteriorNavigation { get; set; }
        public virtual TGestionDocenteEstadoEjecucion IdGestionDocenteEstadoEjecucionNuevoNavigation { get; set; } = null!;
    }
}
