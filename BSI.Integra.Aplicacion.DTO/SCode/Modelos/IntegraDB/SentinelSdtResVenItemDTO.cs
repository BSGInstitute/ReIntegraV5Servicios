namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SentinelSdtResVenItemDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NroDocumento { get; set; }
        public int? CantidadDocs { get; set; }
        public string? Fuente { get; set; }
        public string? Entidad { get; set; }
        public decimal? Monto { get; set; }
        public short? Cantidad { get; set; }
        public int? DiasVencidos { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class SentinelSdtResVenItemComboDTO
    {
        public int Id { get; set; }
        public string? NroDocumento { get; set; }
        public string? Fuente { get; set; }
        public string? Entidad { get; set; }
    }
}
