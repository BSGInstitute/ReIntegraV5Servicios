using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SentinelSdtEstandarItem : BaseIntegraEntity
    {
        public int? IdSentinel { get; set; }
        [StringLength(4)]
        public string? TipoDocumento { get; set; }
        [StringLength(20)]
        public string? Documento { get; set; }
        [StringLength(200)]
        public string? RazonSocial { get; set; }
        public DateTime? FechaProceso { get; set; }
        [StringLength(100)]
        public string? Semaforos { get; set; }
        [StringLength(16)]
        public string? Score { get; set; }
        [StringLength(2)]
        public string? NroBancos { get; set; }
        [StringLength(20)]
        public string? DeudaTotal { get; set; }
        [StringLength(20)]
        public string? VencidoBanco { get; set; }
        [StringLength(100)]
        public string? Calificativo { get; set; }
        [StringLength(40)]
        public string? Veces24m { get; set; }
        [StringLength(50)]
        public string? SemanaActual { get; set; }
        [StringLength(50)]
        public string? SemanaPrevio { get; set; }
        [StringLength(50)]
        public string? SemanaPeorMejor { get; set; }
        [StringLength(20)]
        public string? Documento2 { get; set; }
        [StringLength(20)]
        public string? EstadoDomicilio { get; set; }
        [StringLength(4)]
        public string? CondicionDomicilio { get; set; }
        [StringLength(10)]
        public string? DeudaTributaria { get; set; }
        [StringLength(10)]
        public string? DeudaLaboral { get; set; }
        [StringLength(10)]
        public string? DeudaImpagable { get; set; }
        [StringLength(10)]
        public string? DeudaProtestos { get; set; }
        [StringLength(11)]
        public string? DeudaSbs { get; set; }
        [StringLength(4)]
        public string? CuentasTarjetas { get; set; }
        [StringLength(4)]
        public string? ReporteNegativo { get; set; }
        [StringLength(100)]
        public string? TipoActividad { get; set; }
        public DateTime? FechaInicioActividad { get; set; }
        [StringLength(16)]
        public string? DeudaDirecta { get; set; }
        [StringLength(16)]
        public string? DeudaIndirecta { get; set; }
        [StringLength(16)]
        public string? DeudaCastigada { get; set; }
        [StringLength(20)]
        public string? LineaCreditoNoUtilizada { get; set; }
        [StringLength(20)]
        public string? TotalRiesgo { get; set; }
        [StringLength(200)]
        public string? PeorCalificacion { get; set; }
        [StringLength(20)]
        public string? PorcentajeCalificacionNormal { get; set; }
    }
}
