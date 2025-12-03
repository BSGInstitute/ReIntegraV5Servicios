using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena el detalle de los campos enviados del CRM a Facebook 
    /// </summary>
    public partial class TFacebookFormularioLeadgenLog
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es fk T_FacebookFormularioLeadgen
        /// </summary>
        public int IdFacebookFormularioLeadgen { get; set; }
        /// <summary>
        /// Datos JSON del formulario leadgen obtenidos de Facebook Graph API enviados desce CRM
        /// </summary>
        public string JsonApiFacebook { get; set; } = null!;
        /// <summary>
        /// Respuesta completa de la API de Facebook al enviar/registrar el leadgen
        /// </summary>
        public string RespuestaApiFacebook { get; set; } = null!;
        /// <summary>
        /// Indicador booleano que señala si hubo error en el envío a Facebook (1=Error, 0=Éxito)
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
        /// <summary>
        /// Nombre del evento de Facebook al registrar el leadgen
        /// </summary>
        public string? Evento { get; set; }
        /// <summary>
        /// Pixel de Facebook asociado al evento del leadgen
        /// </summary>
        public string? Pixel { get; set; }

        public virtual TFacebookFormularioLeadgen IdFacebookFormularioLeadgenNavigation { get; set; } = null!;
    }
}
