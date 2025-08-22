using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TChatbotUsuarioRespuestum
    {
        public TChatbotUsuarioRespuestum()
        {
            InverseIdChatbotUsuarioContactoNavigation = new HashSet<TChatbotUsuarioRespuestum>();
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave primaria T_ChatbotUsuarioRespuesta
        /// </summary>
        public int IdChatbotUsuarioContacto { get; set; }
        /// <summary>
        /// Guarda si el usuario esta registrado con un booleano
        /// </summary>
        public bool? UsuarioRegistrado { get; set; }
        public int? IdConfiguracionFlujoChatbot { get; set; }
        /// <summary>
        /// Paso en que se encuentra
        /// </summary>
        public int Paso { get; set; }
        /// <summary>
        /// Caso que selecciono 
        /// </summary>
        public string Caso { get; set; } = null!;
        public string? MensajeEnviado { get; set; }
        public string? OpcionEnviadoJson { get; set; }
        /// <summary>
        /// Respuesta dada por el usuario
        /// </summary>
        public string? Respuesta { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        public int? IdCampoContacto { get; set; }

        public virtual TChatbotUsuarioRespuestum IdChatbotUsuarioContactoNavigation { get; set; } = null!;
        public virtual ICollection<TChatbotUsuarioRespuestum> InverseIdChatbotUsuarioContactoNavigation { get; set; }
    }
}
