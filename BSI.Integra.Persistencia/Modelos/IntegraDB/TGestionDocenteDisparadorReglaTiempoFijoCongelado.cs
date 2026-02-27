using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Copia congelada de la regla de tiempo fijo para disparadores. Define una fecha y hora exacta para la ejecución de la actividad.
    /// </summary>
    public partial class TGestionDocenteDisparadorReglaTiempoFijoCongelado
    {
        /// <summary>
        /// Identificador único de la regla de tiempo fijo congelada. Clave primaria. Generado automáticamente.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador del disparador congelado al cual pertenece esta regla. Clave foránea a T_GestionDocenteDisparadorCongelado.
        /// </summary>
        public int IdGestionDocenteDisparadorCongelado { get; set; }
        /// <summary>
        /// Identificador de la regla de tiempo fijo original. Referencia para auditoría.
        /// </summary>
        public int IdGestionDocenteDisparadorReglaTiempoFijo { get; set; }
        /// <summary>
        /// Identificador de la regla de tiempo genérica. Referencia la configuración base del disparador.
        /// </summary>
        public int IdGestionDocenteDisparadorReglaTiempo { get; set; }
        /// <summary>
        /// Identificador del disparador detalle original. Permite rastrear la estructura original para auditoría.
        /// </summary>
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        /// <summary>
        /// Fecha y hora exacta en UTC-5 de ejecución del disparador. Formato: YYYY-MM-DD HH:MM:SS. Esta es la fecha calculada/asignada para esta instancia específica. Ejemplo: 2026-03-15 14:30:00.
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Identificador del estado de ejecución. Referencia a T_GestionDocenteEstadoEjecucion.
        /// </summary>
        public int IdGestionDocenteEstadoEjecucion { get; set; }
        /// <summary>
        /// Indicador de estado activo/inactivo. 1 = Activo, 0 = Inactivo. Campo de auditoría obligatorio.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que realizó el congelamiento. Máximo 50 caracteres. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Último usuario que modificó el registro. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora en UTC-5 del congelamiento. Campo de auditoría obligatorio.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora en UTC-5 de la última modificación. Campo de auditoría obligatorio.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Control de concurrencia optimista. Generado automáticamente. Campo de auditoría obligatorio.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TGestionDocenteDisparadorCongelado IdGestionDocenteDisparadorCongeladoNavigation { get; set; } = null!;
        public virtual TGestionDocenteDisparadorDetalle IdGestionDocenteDisparadorDetalleNavigation { get; set; } = null!;
        public virtual TGestionDocenteDisparadorReglaTiempoFijo IdGestionDocenteDisparadorReglaTiempoFijoNavigation { get; set; } = null!;
        public virtual TGestionDocenteDisparadorReglaTiempo IdGestionDocenteDisparadorReglaTiempoNavigation { get; set; } = null!;
        public virtual TGestionDocenteEstadoEjecucion IdGestionDocenteEstadoEjecucionNavigation { get; set; } = null!;
    }
}
