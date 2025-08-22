using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena Preguntas junto con las Respuestas de LinkedIn recibidos a travez de la Api de LeadsLinkedIn
    /// </summary>
    public partial class TQuestionLead
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id del Lead asociado
        /// </summary>
        public string? GuidLinkedInLead { get; set; }
        /// <summary>
        /// Id del Formulario asociado
        /// </summary>
        public long? IdLinkedInForm { get; set; }
        /// <summary>
        /// Id del QuestionForm asociado
        /// </summary>
        public int? NroQuestionForm { get; set; }
        public string? Answer { get; set; }
        /// <summary>
        /// Estado del QuestionLead
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario Creacion del QuestionLead
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario Modificacion del QuestionLead
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha Creacion del QuestionLead
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha Modificacion del QuestionLead
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
