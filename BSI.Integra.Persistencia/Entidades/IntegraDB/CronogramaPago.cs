using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CronogramaPago : BaseIntegraEntity
    {
        public int? IdMatriculaCabecera { get; set; }
        public int? IdAlumno { get; set; }
        public int IdPespecifico { get; set; }
        [StringLength(10)]
        public string? Periodo { get; set; }
        [StringLength(20)]
        public string? Moneda { get; set; }
        [StringLength(20)]
        public string? AcuerdoPago { get; set; }
        public double? TipoCambio { get; set; }
        public double? TotalPagar { get; set; }
        public int? NroCuotas { get; set; }
        public DateTime? FechaIniPago { get; set; }
        public bool? Enviado { get; set; }
        [StringLength(300)]
        public string? Observaciones { get; set; }
        public bool? ConCuotaInicial { get; set; }
        public decimal? CuotaInicial { get; set; }
        public bool? CadaNdias { get; set; }
        public int? Ndias { get; set; }
        [StringLength(20)]
        public string? WebMoneda { get; set; }
        public double? WebTipoCambio { get; set; }
        public double? WebTotalPagar { get; set; }
        public double? WebTotalPagarConv { get; set; }
    }
}
