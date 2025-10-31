namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GoogleAds
{
    /// <summary>
    /// DTO: Subcuenta de Google Ads
    /// Autor: Miguel
    /// Fecha: 2025-10-23
    /// </summary>
    public class GoogleAdsSubcuentaDTO
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = null!;
        public string NombreSubcuenta { get; set; } = null!;
        public string? ConversionActionIdIT { get; set; }
        public string? ConversionActionIdIPPF { get; set; }
        public string? ConversionActionIdICISM { get; set; }
        public bool Activo { get; set; }
    }
}
