using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena las configuraciones maestras de las acciones de conversión de Google Ads. Define los parámetros de cada tipo de conversión (nombre, ID de acción, valor base) y controla qué conversiones están activas para ser procesadas y enviadas a Google Ads API
    /// </summary>
    public partial class TGoogleAdsConversionConfiguracion
    {
        /// <summary>
        /// Id del registro
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre descriptivo de la conversión
        /// </summary>
        public string NombreConversion { get; set; } = null!;
        /// <summary>
        /// Es id de la acción de conversión en Google Ads
        /// </summary>
        public string? ConversionActionId { get; set; }
        /// <summary>
        /// Tipo de conversión registrada
        /// </summary>
        public string TipoConversion { get; set; } = null!;
        /// <summary>
        /// Valor base monetario de la conversión
        /// </summary>
        public decimal? ValorConversionBase { get; set; }
        /// <summary>
        /// Indica si la configuración está activa
        /// </summary>
        public bool? Activo { get; set; }
        /// <summary>
        /// Descripción detallada de la conversión
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Indica si el proceso de conversión está activo
        /// </summary>
        public bool? ProcesoActivo { get; set; }
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
        /// Campo de auditoria - RowVersion
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
