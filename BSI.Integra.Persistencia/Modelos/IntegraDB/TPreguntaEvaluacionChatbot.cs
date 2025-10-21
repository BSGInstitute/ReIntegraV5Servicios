using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena las preguntas de evaluación asociadas a formularios específicos
    /// </summary>
    public partial class TPreguntaEvaluacionChatbot
    {
        public TPreguntaEvaluacionChatbot()
        {
            TRespuestaClienteTextoChatbots = new HashSet<TRespuestaClienteTextoChatbot>();
            TRespuestaEvaluacionChatbots = new HashSet<TRespuestaEvaluacionChatbot>();
        }

        /// <summary>
        /// Clave primaria (ID) de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la versión del formulario asociado
        /// </summary>
        public int IdVersionFormularioEvaluacionChatbot { get; set; }
        /// <summary>
        /// Identificador del tipo de entrada para la respuesta
        /// </summary>
        public int IdTipoEntradaEvaluacionChatbot { get; set; }
        /// <summary>
        /// Texto completo de la pregunta de evaluación
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Orden de visualización de la pregunta en el formulario
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

        public virtual TTipoEntradaEvaluacionChatbot IdTipoEntradaEvaluacionChatbotNavigation { get; set; } = null!;
        public virtual TVersionFormularioEvaluacionChatbot IdVersionFormularioEvaluacionChatbotNavigation { get; set; } = null!;
        public virtual ICollection<TRespuestaClienteTextoChatbot> TRespuestaClienteTextoChatbots { get; set; }
        public virtual ICollection<TRespuestaEvaluacionChatbot> TRespuestaEvaluacionChatbots { get; set; }
    }
}
