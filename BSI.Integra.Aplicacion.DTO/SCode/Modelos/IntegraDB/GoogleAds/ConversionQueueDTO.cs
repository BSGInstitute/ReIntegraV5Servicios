namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GoogleAds
{
    /// <summary>
    /// DTO: Datos de conversión en cola
    /// Autor: Sistema
    /// Fecha: 2025-10-04
    /// </summary>
    public class ConversionQueueDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public string Gclid { get; set; } = null!;
        public string TipoConversion { get; set; } = null!;
        public string? EmailHasheado { get; set; }
        public string? CelularHasheado { get; set; }
        public DateTime FechaHoraConversion { get; set; }
        public string FechaHoraConversionFormatoGoogle { get; set; } = null!;
        public decimal? ValorConversion { get; set; }
        public string ConversionActionId { get; set; } = null!;
    }
}
