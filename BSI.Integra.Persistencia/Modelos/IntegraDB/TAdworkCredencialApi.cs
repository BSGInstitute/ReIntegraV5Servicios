using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TAdworkCredencialApi
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string DeveloperToken { get; set; } = null!;
        /// <summary>
        /// Id ClientCustomer
        /// </summary>
        public string ClientCustomerId { get; set; } = null!;
        /// <summary>
        /// Id OAuth2Client 
        /// </summary>
        public string Oauth2ClientId { get; set; } = null!;
        /// <summary>
        /// OAuth2ClientSecret
        /// </summary>
        public string Oauth2ClientSecret { get; set; } = null!;
        /// <summary>
        /// Token OAuth2Refresh
        /// </summary>
        public string Oauth2RefreshToken { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Id de la acción de conversión para Interesado por Trabajar - IT (Fase 13)
        /// </summary>
        public string? ConversionActionIdIt { get; set; }
        /// <summary>
        /// Id de la acción de conversión para Inscripción Proceso Pago Final relacionado con Promesa de Ficha - PF (Fase 22)
        /// </summary>
        public string? ConversionActionIdIppf { get; set; }
        /// <summary>
        /// Id de la acción de conversión para transición entre Inscrito - IS (Fase 5) y Matriculado - M (Fase 23)
        /// </summary>
        public string? ConversionActionIdIcism { get; set; }
        /// <summary>
        /// Indica si el proceso de envío de conversiones está activo
        /// </summary>
        public bool? ProcesoConversionesActivo { get; set; }
        /// <summary>
        /// Versión del API de Google Ads utilizada
        /// </summary>
        public string? ApiVersion { get; set; }
        /// <summary>
        /// Identificador de la cuenta administradora (MCC - My Client Center) de Google Ads
        /// </summary>
        public string? ManagerAccountId { get; set; }
    }
}
