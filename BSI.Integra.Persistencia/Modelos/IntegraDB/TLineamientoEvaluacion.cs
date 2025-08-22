using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que almacena los Lineamiento de Evaluaciones con información relevante como nombre, criticidad que representa el nivel critico del LineamientoEvaluacion y estado.
    /// </summary>
    public partial class TLineamientoEvaluacion
    {
        public TLineamientoEvaluacion()
        {
            TCriterioLineamientoDetalles = new HashSet<TCriterioLineamientoDetalle>();
        }

        /// <summary>
        /// Identificador único generado automáticamente para cada registro.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del LineamientoEvaluacion. No puede ser nulo.
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Nivel de criticidad del LineamientoEvaluacion. No puede ser nulo.
        /// </summary>
        public int Criticidad { get; set; }
        /// <summary>
        /// Estado del LineamientoEvaluacion. Por defecto es 1 (activo).
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro. No puede ser nulo.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro. Por defecto es la fecha y hora actual.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario que modificó el registro. Puede ser nulo.
        /// </summary>
        public string? UsuarioModificacion { get; set; }
        /// <summary>
        /// Fecha de modificación del registro. Puede ser nulo.
        /// </summary>
        public DateTime? FechaModificacion { get; set; }
        /// <summary>
        /// Versión de la fila para control de concurrencia.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TCriterioLineamientoDetalle> TCriterioLineamientoDetalles { get; set; }
    }
}
