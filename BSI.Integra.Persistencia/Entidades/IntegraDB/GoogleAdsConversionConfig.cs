using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    /// <summary>
    /// Entidad: Configuración de acciones de conversión de Google Ads
    /// Tabla: mkt.T_GoogleAdsConversionConfig
    /// Autor: Sistema
    /// Fecha: 2025-10-04
    /// </summary>
    [Table("T_GoogleAdsConversionConfiguracion", Schema = "mkt")]
    public class GoogleAdsConversionConfig : BaseIntegraEntity
    {
        public string NombreConversion { get; set; } = null!;
        public string? ConversionActionId { get; set; }
        public string TipoConversion { get; set; } = null!;
        public decimal? ValorConversionBase { get; set; }
        public bool Activo { get; set; } = true;
        public string? Descripcion { get; set; }
        public bool ProcesoActivo { get; set; } = true;
    }
}
