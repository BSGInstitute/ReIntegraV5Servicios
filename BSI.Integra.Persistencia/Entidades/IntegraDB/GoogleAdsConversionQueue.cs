using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    /// <summary>
    /// Entidad: Cola de conversiones offline pendientes de envío a Google Ads
    /// Tabla: mkt.T_GoogleAdsConversionQueue
    /// Autor: Sistema
    /// Fecha: 2025-10-04
    /// </summary>
    [Table("T_GoogleAdsConversionQueue", Schema = "mkt")]
    public class GoogleAdsConversionQueue : BaseIntegraEntity
    {
        // Identificadores
        public int IdOportunidad { get; set; }
        public int IdAlumno { get; set; }
        public int? IdGoogleFormularioLeadgen { get; set; }

        // Datos de Google Ads
        public string? Gclid { get; set; }
        public string? CampaniaGoogle { get; set; }
        public string? FormularioGoogle { get; set; }
        public string? GrupoAds { get; set; }

        // Información de origen
        public int? IdOrigen { get; set; }
        public int? IdCategoriaOrigen { get; set; }

        // Transición de fase
        public int? IdFaseOportunidadAnterior { get; set; }
        public int IdFaseOportunidadActual { get; set; }
        public string? NombreFaseAnterior { get; set; }
        public string NombreFaseActual { get; set; } = null!;

        // Tipo de conversión
        public string TipoConversion { get; set; } = null!;
        public string? ConversionActionName { get; set; }
        public string? ConversionActionId { get; set; }

        // Datos del contacto (hasheados con SHA-256)
        public string? Email { get; set; }
        public string? EmailHasheado { get; set; }
        public string? Celular { get; set; }
        public string? CelularHasheado { get; set; }

        // Información de conversión
        public DateTime FechaHoraConversion { get; set; }
        public string? FechaHoraConversionFormatoGoogle { get; set; }
        public decimal? ValorConversion { get; set; }

        // Control de envío
        public string EstadoEnvio { get; set; } = "Pendiente";
        public int IntentosEnvio { get; set; } = 0;
        public DateTime? FechaEnvio { get; set; }
        public string? RespuestaGoogleAds { get; set; }
        public string? MensajeError { get; set; }

        // Validaciones
        public bool EsOrigenGoogle { get; set; } = false;
        public bool TieneGclid { get; set; } = false;
        public bool TieneEmail { get; set; } = false;
        public bool TieneCelular { get; set; } = false;
        public bool EsValidoParaEnvio { get; set; } = false;
        public string? MotivoDescarte { get; set; }
    }
}
