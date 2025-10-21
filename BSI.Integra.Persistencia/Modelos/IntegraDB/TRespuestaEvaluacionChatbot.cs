using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena las respuestas predefinidas para las preguntas de evaluación
    /// </summary>
    public partial class TRespuestaEvaluacionChatbot
    {
        public TRespuestaEvaluacionChatbot()
        {
            TProblemaIdentificadoChatbots = new HashSet<TProblemaIdentificadoChatbot>();
            TRespuestaClienteChatbots = new HashSet<TRespuestaClienteChatbot>();
        }

        /// <summary>
        /// Clave primaria (ID) de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la pregunta de evaluación asociada
        /// </summary>
        public int IdPreguntaEvaluacionChatbot { get; set; }
        /// <summary>
        /// Identificador del tipo de entrada de la respuesta
        /// </summary>
        public int IdTipoEntradaEvaluacionChatbot { get; set; }
        /// <summary>
        /// Valor de la respuesta predefinida
        /// </summary>
        public string Respuesta { get; set; } = null!;
        /// <summary>
        /// Orden de visualización de la respuesta en la lista
        /// </summary>
        public int Orden { get; set; }
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

        public virtual TPreguntaEvaluacionChatbot IdPreguntaEvaluacionChatbotNavigation { get; set; } = null!;
        public virtual TTipoEntradaEvaluacionChatbot IdTipoEntradaEvaluacionChatbotNavigation { get; set; } = null!;
        public virtual ICollection<TProblemaIdentificadoChatbot> TProblemaIdentificadoChatbots { get; set; }
        public virtual ICollection<TRespuestaClienteChatbot> TRespuestaClienteChatbots { get; set; }
    }
}
