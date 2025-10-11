namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GoogleAds
{
    /// <summary>
    /// DTO: Credenciales de Google Ads API
    /// Autor: Sistema
    /// Fecha: 2025-10-04
    /// </summary>
    public class AdwordsCredencialesDTO
    {
        public string DeveloperToken { get; set; } = null!;
        public string CustomerId { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public string? ConversionActionIdIT { get; set; }
        public string? ConversionActionIdIPPF { get; set; }
        public string? ConversionActionIdICISM { get; set; }
        public bool ProcesoActivo { get; set; }
        public string ApiVersion { get; set; } = "v20";
    }
}
