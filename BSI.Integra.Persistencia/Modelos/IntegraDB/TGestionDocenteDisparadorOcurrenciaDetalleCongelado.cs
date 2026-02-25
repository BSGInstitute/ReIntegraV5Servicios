using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Copia congelada de disparadores que dependen de ocurrencias previas. Define que una actividad se ejecuta solo después de que otra ocurrencia ha sido marcada.
    /// </summary>
    public partial class TGestionDocenteDisparadorOcurrenciaDetalleCongelado
    {
        /// <summary>
        /// Identificador único. Clave primaria. Generado automáticamente.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador del disparador congelado. Clave foránea a T_GestionDocenteDisparadorCongelado.
        /// </summary>
        public int IdGestionDocenteDisparadorCongelado { get; set; }
        /// <summary>
        /// Identificador del disparador por ocurrencia original. Referencia para auditoría.
        /// </summary>
        public int IdGestionDocenteDisparadorOcurrenciaDetalle { get; set; }
        /// <summary>
        /// Identificador del disparador detalle original. Referencia para auditoría.
        /// </summary>
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        /// <summary>
        /// Identificador de la ocurrencia congelada que debe ocurrir primero. Establece la dependencia. Clave foránea a T_GestionDocenteOcurrenciaCongelada. Esta ocurrencia DEBE ser marcada antes de que se ejecute la actividad.
        /// </summary>
        public int IdGestionDocenteOcurrenciaCongeladaPrevia { get; set; }
        /// <summary>
        /// Identificador del estado de ejecución. Referencia a T_GestionDocenteEstadoEjecucion.
        /// </summary>
        public int IdGestionDocenteEstadoEjecucion { get; set; }
        /// <summary>
        /// Indicador de estado activo/inactivo. Campo de auditoría obligatorio.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que realizó el congelamiento. Campo de auditoría obligatorio.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Último usuario que modificó. Campo de auditoría obligatorio.
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
        /// Control de concurrencia optimista. Campo de auditoría obligatorio.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TGestionDocenteDisparadorCongelado IdGestionDocenteDisparadorCongeladoNavigation { get; set; } = null!;
        public virtual TGestionDocenteDisparadorDetalle IdGestionDocenteDisparadorDetalleNavigation { get; set; } = null!;
        public virtual TGestionDocenteDisparadorOcurrenciaDetalle IdGestionDocenteDisparadorOcurrenciaDetalleNavigation { get; set; } = null!;
        public virtual TGestionDocenteEstadoEjecucion IdGestionDocenteEstadoEjecucionNavigation { get; set; } = null!;
        public virtual TGestionDocenteOcurrenciaCongeladum IdGestionDocenteOcurrenciaCongeladaPreviaNavigation { get; set; } = null!;
    }
}
