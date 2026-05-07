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
        /// Identificador del chat del portal asociado (nullable: registros WhatsApp no lo usan)
        /// </summary>
        public int? IdChatbotPortalHiloChat { get; set; }
        /// <summary>
        /// Identificador del medio de comunicación (FK a pla.T_MedioComunicacion)
        /// </summary>
        public int? IdMedioComunicacion { get; set; }
        /// <summary>
        /// ID polimórfico del hilo según canal: Portal → T_ChatbotPortalHiloChat.Id, WhatsApp → T_ChatbotWhatsAppAtcHiloChat.Id
        /// </summary>
        public int? IdOriginal { get; set; }
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

        public virtual TChatbotPortalHiloChat? IdChatbotPortalHiloChatNavigation { get; set; }
        public virtual TVersionFormularioEvaluacionChatbot IdVersionFormularioEvaluacionChatbotNavigation { get; set; } = null!;
        public virtual ICollection<TProblemaIdentificadoChatbot> TProblemaIdentificadoChatbots { get; set; }
        public virtual ICollection<TRespuestaClienteChatbot> TRespuestaClienteChatbots { get; set; }
        public virtual ICollection<TRespuestaClienteTextoChatbot> TRespuestaClienteTextoChatbots { get; set; }
    }
}
