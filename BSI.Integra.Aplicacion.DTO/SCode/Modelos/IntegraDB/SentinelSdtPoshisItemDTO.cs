namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SentinelSdtPoshisItemDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }
        public DateTime? FechaProceso { get; set; }
        public string? SemanaActual { get; set; }
        public string? DescripcionSemaforo { get; set; }
        public decimal? Score { get; set; }
        public int? CodigoVariacion { get; set; }
        public string? DescripcionVariacion { get; set; }
        public int? NumeroEntidades { get; set; }
        public decimal? DeudaTotal { get; set; }
        public decimal? PorcentajeCalificacion { get; set; }
        public int? PeorCalificacion { get; set; }
        public string? PeroCalificacionDescripcion { get; set; }
        public decimal? MontoSbs { get; set; }
        public decimal? ProgresoRegistro { get; set; }
        public decimal? DocImpuesto { get; set; }
        public decimal? DeudaTributaria { get; set; }
        public decimal? Afp { get; set; }
        public int? TarjetaCredito { get; set; }
        public int? CuentaCorriente { get; set; }
        public int? ReporteNegativo { get; set; }
        public decimal? DeudaDirecta { get; set; }
        public decimal? DeudaIndirecta { get; set; }
        public decimal? LineaCreditoNoUtilizada { get; set; }
        public decimal? DeudaCastigada { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class SentinelSdtPoshisItemComboDTO
    {
        public int Id { get; set; }
        public string? NumeroDocumento { get; set; }
        public DateTime? FechaProceso { get; set; }

    }
    public class SentinelSdtPoshisItemPosicionHistoriaDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public DateTime FechaProceso { get; set; }
        public string SemanaActual { get; set; }
        public decimal? Score { get; set; }
        public int? CodigoVariacion { get; set; }
        public int? NumeroEntidades { get; set; }
        public decimal? DeudaTotal { get; set; }
        public decimal? ProgresoRegistro { get; set; }
        public decimal? DocImpuesto { get; set; }
        public decimal? DeudaTributaria { get; set; }
        public string DeudaLab { get; set; }
        public int? CuentaCorriente { get; set; }
        public int? TarjetaCredito { get; set; }
        public int? ReporteNegativo { get; set; }
        public decimal? PorcentajeCalificacion { get; set; }
        public int? PeorCalificacion { get; set; }
    }
}
