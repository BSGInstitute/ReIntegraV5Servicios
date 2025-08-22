namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SentinelRepLegItemDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string TipoDocumento { get; set; } = null!;
        public string NumeroDocumento { get; set; } = null!;
        public string? Nombres { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? RazonSocial { get; set; }
        public string? Cargo { get; set; }
        public string SemaforoActual { get; set; } = null!;
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class SentinelRepLegItemComboDTO
    {
        public int Id { get; set; }
        public string NumeroDocumento { get; set; } = null!;
        public string RazonSocial { get; set; } = null!;
    }
}
