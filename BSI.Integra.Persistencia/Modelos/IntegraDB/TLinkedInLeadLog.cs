using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena el detalle de los campos enviados del CRM a Linkedin 
    /// </summary>
    public partial class TLinkedInLeadLog
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es fk T_LinkedInLead
        /// </summary>
        public int IdLinkedInLead { get; set; }
        /// <summary>
        /// Datos JSON enviados a LinkedIn con la información del lead.
        /// </summary>
        public string JsonApiLinkedin { get; set; } = null!;
        /// <summary>
        /// Respuesta JSON recibida de LinkedIn con el resultado del envío.
        /// </summary>
        public string RespuestaApiLinkedin { get; set; } = null!;
        /// <summary>
        /// Indicador booleano que señala si hubo error en el envío a Linkedin (1=Error, 0=Éxito)
        /// </summary>
        public bool EsError { get; set; }
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TLinkedInLead IdLinkedInLeadNavigation { get; set; } = null!;
    }
}
