namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SentinelSdtRepSbsitemDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string? TipoDoc { get; set; }
        public string? NroDoc { get; set; }
        public string? NombreRazonSocial { get; set; }
        public string? Calificacion { get; set; }
        public decimal? MontoDeuda { get; set; }
        public int? DiasVencidos { get; set; }
        public DateTime? FechaReporte { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class SentinelSdtRepSbsitemComboDTO
    {
        public int Id { get; set; }
        public string? NroDoc { get; set; }
        public string? NombreRazonSocial { get; set; }

    }
    public class SentinelSdtRepSbsitemLineaDeudaDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string TipoDoc { get; set; }
        public string NombreRazonSocial { get; set; }
        public string Calificacion { get; set; }
        public decimal? MontoDeuda { get; set; }
        public int? DiasVencidos { get; set; }
        public DateTime? FechaReporte { get; set; }
    }
}
