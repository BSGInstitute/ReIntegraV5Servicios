namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GoogleAds
{
    /// <summary>
    /// DTO: Resultado del proceso de envío de conversiones
    /// Autor: Miguel
    /// Fecha: 2025-10-04
    /// </summary>
    public class ConversionResultDTO
    {
        public string Mensaje { get; set; } = null!;
        public int Total { get; set; }
        public int Exitosas { get; set; }
        public int Errores { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
