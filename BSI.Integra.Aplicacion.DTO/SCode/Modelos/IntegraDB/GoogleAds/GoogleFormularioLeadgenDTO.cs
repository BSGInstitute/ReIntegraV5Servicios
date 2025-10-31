namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GoogleAds
{
    /// <summary>
    /// DTO: Lead de formulario de Google Ads
    /// Autor: Miguel
    /// Fecha: 2025-10-23
    /// </summary>
    public class GoogleFormularioLeadgenDTO
    {
        public int Id { get; set; }
        public string CampaniaGoogle { get; set; } = null!;
        public string? FormularioGoogle { get; set; }
        public string? Gclid { get; set; }
    }
}
