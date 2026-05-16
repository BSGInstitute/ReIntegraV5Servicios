using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Relaciona chats con formularios de evaluación aplicados
    /// </summary>
    public partial class TFormularioAplicadoChatbot
    {
        public TFormularioAplicadoChatbot()
        {
            TProblemaIdentificadoChatbots = new HashSet<TProblemaIdentificadoChatbot>();
            TRespuestaClienteChatbots = new HashSet<TRespuestaClienteChatbot>();
            TRespuestaClienteTextoChatbots = new HashSet<TRespuestaClienteTextoChatbot>();
        }

        /// <summary>
        /// Clave primaria (ID) de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador del chat del portal asociado
        /// </summary>
        public int IdChatbotPortalHiloChat { get; set; }
        /// <summary>
        /// Identificador de la versión del formulario aplicado
        /// </summary>
        public int IdVersionFormularioEvaluacionChatbot { get; set; }
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
        /// <summary>
        /// Canal de comunicación del hilo (Portal Web, WhatsApp, etc.), JOIN a una de esas tablas por el campo IdOriginal
        /// </summary>
        public int? IdMedioComunicacion { get; set; }
        /// <summary>
        /// ID del hilo en la tabla origen según el medio de comunicación (polimórfico)
        /// </summary>
        public int? IdOriginal { get; set; }

        public virtual TChatbotPortalHiloChat IdChatbotPortalHiloChatNavigation { get; set; } = null!;
        public virtual TMedioComunicacion? IdMedioComunicacionNavigation { get; set; }
        public virtual TVersionFormularioEvaluacionChatbot IdVersionFormularioEvaluacionChatbotNavigation { get; set; } = null!;
        public virtual ICollection<TProblemaIdentificadoChatbot> TProblemaIdentificadoChatbots { get; set; }
        public virtual ICollection<TRespuestaClienteChatbot> TRespuestaClienteChatbots { get; set; }
        public virtual ICollection<TRespuestaClienteTextoChatbot> TRespuestaClienteTextoChatbots { get; set; }
    }
}
