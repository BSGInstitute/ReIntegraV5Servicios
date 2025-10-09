using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena Leads de LinkedIn recibidos a travez de la Api de LeadsLinkedIn
    /// </summary>
    public partial class TLinkedInLead
    {
        public TLinkedInLead()
        {
            TLinkedInLeadLogs = new HashSet<TLinkedInLeadLog>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id que viene del Api de Linkedin para Leads
        /// </summary>
        public string? GuidLinkedInLead { get; set; }
        /// <summary>
        /// Id del Formulario que envia el Api de Leads de Linkedin
        /// </summary>
        public long? IdLinkedInForm { get; set; }
        /// <summary>
        /// Id de la Campanha que envia el Api de Leads de Linkedin
        /// </summary>
        public long? IdLinkedInCampaign { get; set; }
        /// <summary>
        /// El tipo de Lead que recibio
        /// </summary>
        public string? LeadType { get; set; }
        /// <summary>
        /// Indica si es de Prueba
        /// </summary>
        public string? TestLead { get; set; }
        /// <summary>
        /// Preguntas del Lead
        /// </summary>
        public string? Question { get; set; }
        /// <summary>
        /// Fecha que se genero el Lead
        /// </summary>
        public DateTime? FechaLead { get; set; }
        /// <summary>
        /// Estado de la Oportunidad del Lead
        /// </summary>
        public bool? OportunidadRegistrada { get; set; }
        /// <summary>
        /// Estado del Lead
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario Creacion del Lead
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario Modificacion del Lead
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha Creacion del Lead
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha Modificacion del Lead
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual ICollection<TLinkedInLeadLog> TLinkedInLeadLogs { get; set; }
    }
}
