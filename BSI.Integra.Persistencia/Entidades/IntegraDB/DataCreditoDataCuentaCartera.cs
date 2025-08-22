using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataCuentaCartera : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        public bool? Bloqueada { get; set; }
        [StringLength(50)]
        public string? Entidad { get; set; }
        [StringLength(25)]
        public string? Numero { get; set; }
        public DateTime? FechaApertura { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        [StringLength(60)]
        public string? Comportamiento { get; set; }
        [StringLength(20)]
        public string? FormaPago { get; set; }
        public decimal? ProbabilidadIncumplimiento { get; set; }
        [StringLength(20)]
        public string? Calificacion { get; set; }
        [StringLength(20)]
        public string? SituacionTitular { get; set; }
        [StringLength(50)]
        public string? Oficina { get; set; }
        [StringLength(50)]
        public string? Ciudad { get; set; }
        [StringLength(20)]
        public string? CodigoDaneCiudad { get; set; }
        [StringLength(20)]
        public string? CodigoSuscriptor { get; set; }
        public int? TipoIdentificacion { get; set; }
        [StringLength(20)]
        public string? Identificacion { get; set; }
        [StringLength(20)]
        public string? Sector { get; set; }
        public bool? CalificacionHd { get; set; }
        [StringLength(20)]
        public string? CaracteristicaTipoCuenta { get; set; }
        [StringLength(20)]
        public string? CaracteristicaTipoObligacion { get; set; }
        [StringLength(20)]
        public string? CaracteristicaTipoContrato { get; set; }
        [StringLength(20)]
        public string? CaracteristicaEjecucionContrato { get; set; }
        [StringLength(20)]
        public string? CaracteristicaMesesPermanencia { get; set; }
        [StringLength(20)]
        public string? CaracteristicaCalidadDeudor { get; set; }
        [StringLength(20)]
        public string? CaracteristicaGarantia { get; set; }
        [StringLength(20)]
        public string? ValorMoneda { get; set; }
        public DateTime? ValorFecha { get; set; }
        [StringLength(20)]
        public string? ValorCalificacion { get; set; }
        public decimal? ValorSaldoActual { get; set; }
        public decimal? ValorSaldoMora { get; set; }
        public decimal? ValorDisponible { get; set; }
        public decimal? ValorCuota { get; set; }
        public decimal? ValorCuotasMora { get; set; }
        public int? ValorDiasMora { get; set; }
        public DateTime? ValorFechaPagoCuota { get; set; }
        public DateTime? ValorFechaLimitePago { get; set; }
        [StringLength(20)]
        public string? ValorPeriodicidad { get; set; }
        [StringLength(20)]
        public string? ValorTotalCuotas { get; set; }
        public decimal? ValorValorInicial { get; set; }
        [StringLength(20)]
        public string? ValorCuotasCanceladas { get; set; }
        public string? EstadoCuentaCodigo { get; set; }
        [StringLength(20)]
        public DateTime? EstadoCuentaFecha { get; set; }
        [StringLength(20)]
        public string? EstadoOrigenCodigo { get; set; }
        public DateTime? EstadoOrigenFecha { get; set; }
        [StringLength(20)]
        public string? EstadoPagoCodigo { get; set; }
        public DateTime? EstadoPagoFecha { get; set; }
        [StringLength(60)]
        public string? Llave { get; set; }
        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
