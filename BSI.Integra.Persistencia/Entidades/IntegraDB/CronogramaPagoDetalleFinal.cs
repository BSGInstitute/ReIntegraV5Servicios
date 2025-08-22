using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CronogramaPagoDetalleFinal : BaseIntegraEntity
    {
        public int? IdMatriculaCabecera { get; set; }
        public int? NroCuota { get; set; }
        public int? NroSubCuota { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal? TotalPagar { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? Saldo { get; set; }
        public decimal? Mora { get; set; }
        public decimal? MontoPagado { get; set; }
        public bool? Cancelado { get; set; }
        [StringLength(20)]
        public string? TipoCuota { get; set; }
        [StringLength(20)]
        public string? Moneda { get; set; }
        public DateTime? FechaPago { get; set; }
        public int? IdFormaPago { get; set; }
        public int? IdCuenta { get; set; }
        public DateTime? FechaPagoBanco { get; set; }
        public bool? Enviado { get; set; }
        [StringLength(500)]
        public string? Observaciones { get; set; }
        public int? IdDocumentoPago { get; set; }
        [StringLength(50)]
        public string? NroDocumento { get; set; }
        [StringLength(20)]
        public string? MonedaPago { get; set; }
        public decimal? TipoCambio { get; set; }
        public decimal? CuotaDolares { get; set; }
        public DateTime? FechaProcesoPago { get; set; }
        public int? Version { get; set; }
        public bool? Aprobado { get; set; }
        public DateTime? FechaDeposito { get; set; }
        public DateTime? FechaProcesoPagoReal { get; set; }
        public DateTime? FechaIngresoEnCuenta { get; set; }
        public DateTime? FechaEfectivoDisponible { get; set; }
        public decimal? MoraTarifario { get; set; }
        public DateTime? FechaCompromiso1 { get; set; }
        public DateTime? FechaCompromiso2 { get; set; }
        public DateTime? FechaCompromiso3 { get; set; }
        public DateTime? FechaGeneracionCompromiso1 { get; set; }
        public DateTime? FechaGeneracionCompromiso2 { get; set; }
        public DateTime? FechaGeneracionCompromiso3 { get; set; }
        public int? IdPersonalCoordinadorCobranza { get; set; }
        [StringLength(20)]
        public string? UsuarioCoordinadorAcademico { get; set; }
        [StringLength(20)]
        public string? MonedaMoraTarifario { get; set; }

        public int? IdTipoComprobante { get; set; }
        public string? NroDocumentoComprobante { get; set; }
        public string? NombreRazonSocial { get; set; }
        public string? Observacion { get; set; }
    }
}
