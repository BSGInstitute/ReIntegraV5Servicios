namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GoogleAds
{
    /// <summary>
    /// DTO: Estado de conversiones por tipo
    /// Autor: Miguel
    /// Fecha: 2025-10-04
    /// </summary>
    public class ConversionEstadoDTO
    {
        public string Estado { get; set; } = null!;
        public int Cantidad { get; set; }
        public DateTime MasAntigua { get; set; }
        public DateTime MasReciente { get; set; }
    }
}
