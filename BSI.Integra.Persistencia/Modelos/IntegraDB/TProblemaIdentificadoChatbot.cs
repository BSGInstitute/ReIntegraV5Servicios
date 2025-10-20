using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Almacena múltiples problemas identificados por clientes en evaluaciones
    /// </summary>
    public partial class TProblemaIdentificadoChatbot
    {
        /// <summary>
        /// Clave primaria (ID) de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la respuesta de evaluación que representa el problema
        /// </summary>
        public int IdRespuestaEvaluacionChatbot { get; set; }
        /// <summary>
        /// Identificador del formulario aplicado al chat
        /// </summary>
        public int IdFormularioAplicadoChatbot { get; set; }
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

        public virtual TFormularioAplicadoChatbot IdFormularioAplicadoChatbotNavigation { get; set; } = null!;
        public virtual TRespuestaEvaluacionChatbot IdRespuestaEvaluacionChatbotNavigation { get; set; } = null!;
    }
}
