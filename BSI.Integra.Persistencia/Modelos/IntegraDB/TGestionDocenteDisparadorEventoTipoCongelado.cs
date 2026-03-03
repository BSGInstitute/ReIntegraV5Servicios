using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Copia congelada de los tipos de eventos que pueden servir como disparadores del sistema. Define eventos externos que desencadenan actividades. Ejemplo: Asistencia registrada, Calificación publicada, etc.
    /// </summary>
    public partial class TGestionDocenteDisparadorEventoTipoCongelado
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
        /// Identificador del tipo de evento original. Referencia para auditoría.
        /// </summary>
        public int IdGestionDocenteDisparadorEventoTipo { get; set; }
        /// <summary>
        /// Identificador del disparador detalle original. Referencia para auditoría.
        /// </summary>
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        /// <summary>
        /// Nombre del tipo de evento. Máximo 200 caracteres. Ejemplo: &quot;Asistencia registrada&quot;, &quot;Calificación publicada&quot;, &quot;Comentario recibido&quot;, &quot;Tarea entregada&quot;.
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción del evento y cuándo se dispara. Máximo 500 caracteres. Opcional. Ejemplo: &quot;Se dispara cuando un estudiante registra su asistencia en el sistema&quot;.
        /// </summary>
        public string? Descripcion { get; set; }
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
        public virtual TGestionDocenteDisparadorEventoTipo IdGestionDocenteDisparadorEventoTipoNavigation { get; set; } = null!;
        public virtual TGestionDocenteEstadoEjecucion IdGestionDocenteEstadoEjecucionNavigation { get; set; } = null!;
    }
}
