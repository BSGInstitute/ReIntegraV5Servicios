using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena los tipos de entrada disponibles para las respuestas de evaluación
    /// </summary>
    public partial class TTipoEntradaEvaluacionChatbot
    {
        public TTipoEntradaEvaluacionChatbot()
        {
            TPreguntaEvaluacionChatbots = new HashSet<TPreguntaEvaluacionChatbot>();
            TRespuestaEvaluacionChatbots = new HashSet<TRespuestaEvaluacionChatbot>();
        }

        /// <summary>
        /// Clave primaria (ID) de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del tipo de entrada (Ej: Selección Múltiple, Texto Libre, Rating)
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción detallada del tipo de entrada
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Estado del registro (Activo = 1 / Inactivo = 0)
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
        /// Fecha y hora de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la última modificación
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Versión de fila para concurrencia optimista
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TPreguntaEvaluacionChatbot> TPreguntaEvaluacionChatbots { get; set; }
        public virtual ICollection<TRespuestaEvaluacionChatbot> TRespuestaEvaluacionChatbots { get; set; }
    }
}
