using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SentinelSdtPoshisItem : BaseIntegraEntity
    {
        public int? IdSentinel { get; set; }
        [StringLength(20)]
        public string? TipoDocumento { get; set; }
        [StringLength(20)]
        public string? NumeroDocumento { get; set; }
        public DateTime? FechaProceso { get; set; }
        [StringLength(20)]
        public string? SemanaActual { get; set; }
        [StringLength(500)]
        public string? DescripcionSemaforo { get; set; }
        public decimal? Score { get; set; }
        public int? CodigoVariacion { get; set; }
        [StringLength(500)]
        public string? DescripcionVariacion { get; set; }
        public int? NumeroEntidades { get; set; }
        public decimal? DeudaTotal { get; set; }
        public decimal? PorcentajeCalificacion { get; set; }
        public int? PeorCalificacion { get; set; }
        [StringLength(500)]
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
    }
}
