using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla funciona como registro maestro para identificar oportunidades elegibles para el envío de conversiones offline a través de la Google Ads API, capturando información clave como GCLID, campaña, formulario y grupo de anuncios
    /// </summary>
    public partial class TGoogleAdsConversionQueue
    {
        /// <summary>
        /// Id del registro
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id de la oportunidad asociada
        /// </summary>
        public int IdOportunidad { get; set; }
        /// <summary>
        /// Id del alumno asociado
        /// </summary>
        public int IdAlumno { get; set; }
        /// <summary>
        /// Id del formulario de Google del que proviene el lead
        /// </summary>
        public int? IdGoogleFormularioLeadgen { get; set; }
        /// <summary>
        /// Google Click Identifier de adwords
        /// </summary>
        public string? Gclid { get; set; }
        /// <summary>
        /// Id de la campaña de Google Ads, se compone de caracteres
        /// </summary>
        public string? CampaniaGoogle { get; set; }
        /// <summary>
        /// Id del formulario de Google, se compone de caracteres
        /// </summary>
        public string? FormularioGoogle { get; set; }
        /// <summary>
        /// Id del grupo de ads, se compone de caracteres
        /// </summary>
        public string? GrupoAds { get; set; }
        /// <summary>
        /// Id del origen de la oportunidad
        /// </summary>
        public int? IdOrigen { get; set; }
        /// <summary>
        /// Id de la categoría de origen de la oportunidad
        /// </summary>
        public int? IdCategoriaOrigen { get; set; }
        /// <summary>
        /// Id de la fase anterior de la oportunidad
        /// </summary>
        public int? IdFaseOportunidadAnterior { get; set; }
        /// <summary>
        /// Id de la fase actual de la oportunidad
        /// </summary>
        public int IdFaseOportunidadActual { get; set; }
        /// <summary>
        /// Nombre de la fase anterior de la oportunidad
        /// </summary>
        public string? NombreFaseAnterior { get; set; }
        /// <summary>
        /// Nombre de la fase actual de la oportunidad
        /// </summary>
        public string NombreFaseActual { get; set; } = null!;
        /// <summary>
        /// Tipo de conversión registrada
        /// </summary>
        public string TipoConversion { get; set; } = null!;
        /// <summary>
        /// Nombre de la acción de conversión en Google Ads
        /// </summary>
        public string? ConversionActionName { get; set; }
        /// <summary>
        /// Id de la acción de conversión en Google Ads
        /// </summary>
        public string? ConversionActionId { get; set; }
        /// <summary>
        /// Email del contacto en texto plano
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Email del contacto hasheado con SHA-256
        /// </summary>
        public string? EmailHasheado { get; set; }
        /// <summary>
        /// Celular del contacto en texto plano
        /// </summary>
        public string? Celular { get; set; }
        /// <summary>
        /// Celular del contacto hasheado con SHA-256
        /// </summary>
        public string? CelularHasheado { get; set; }
        /// <summary>
        /// Fecha y hora en que ocurrió la conversión
        /// </summary>
        public DateTime FechaHoraConversion { get; set; }
        /// <summary>
        /// Fecha y hora de conversión en formato requerido por Google Ads
        /// </summary>
        public string? FechaHoraConversionFormatoGoogle { get; set; }
        /// <summary>
        /// Valor monetario de la conversión
        /// </summary>
        public decimal? ValorConversion { get; set; }
        /// <summary>
        /// Estado del envío a Google Ads (Pendiente, Enviado, Error)
        /// </summary>
        public string EstadoEnvio { get; set; } = null!;
        /// <summary>
        /// Número de intentos de envío realizados
        /// </summary>
        public int? IntentosEnvio { get; set; }
        /// <summary>
        /// Fecha y hora del último envío a Google Ads
        /// </summary>
        public DateTime? FechaEnvio { get; set; }
        /// <summary>
        /// Respuesta recibida de Google Ads API
        /// </summary>
        public string? RespuestaGoogleAds { get; set; }
        /// <summary>
        /// Mensaje de error en caso de fallo en el envío
        /// </summary>
        public string? MensajeError { get; set; }
        /// <summary>
        /// Indica si el origen de la oportunidad es Google
        /// </summary>
        public bool EsOrigenGoogle { get; set; }
        /// <summary>
        /// Indica si el registro tiene Gclid
        /// </summary>
        public bool TieneGclid { get; set; }
        /// <summary>
        /// Indica si el registro tiene email
        /// </summary>
        public bool TieneEmail { get; set; }
        /// <summary>
        /// Indica si el registro tiene celular
        /// </summary>
        public bool TieneCelular { get; set; }
        /// <summary>
        /// Indica si el registro cumple con todos los requisitos para envío
        /// </summary>
        public bool EsValidoParaEnvio { get; set; }
        /// <summary>
        /// Motivo por el cual el registro fue descartado para envío
        /// </summary>
        public string? MotivoDescarte { get; set; }
        /// <summary>
        /// Estado del registro (activo o eliminado)
        /// </summary>
        public bool? Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de auditoria RowVersion
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TAlumno IdAlumnoNavigation { get; set; } = null!;
        public virtual TCategoriaOrigen? IdCategoriaOrigenNavigation { get; set; }
        public virtual TFaseOportunidad IdFaseOportunidadActualNavigation { get; set; } = null!;
        public virtual TFaseOportunidad? IdFaseOportunidadAnteriorNavigation { get; set; }
        public virtual TGoogleFormularioLeadgen? IdGoogleFormularioLeadgenNavigation { get; set; }
        public virtual TOportunidad IdOportunidadNavigation { get; set; } = null!;
        public virtual TOrigen? IdOrigenNavigation { get; set; }
    }
}
