namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SentinelSdtLincreItemDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? CnsEntNomRazLn { get; set; }
        public string? TipoCuenta { get; set; }
        public decimal? LineaCredito { get; set; }
        public decimal? LineaCreditoNoUtil { get; set; }
        public decimal? LineaUtil { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class SentinelSdtLincreItemComboDTO
    {
        public int Id { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? CnsEntNomRazLn { get; set; }
    }
    public class AlumnosSentinelLineasCreditoDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? CnsEntNomRazLn { get; set; }
        public string? TipoCuenta { get; set; }
        public decimal? LineaCredito { get; set; }
        public decimal? LineaCreditoNoUtil { get; set; }
        public decimal? LineaUtil { get; set; }
    }
}
