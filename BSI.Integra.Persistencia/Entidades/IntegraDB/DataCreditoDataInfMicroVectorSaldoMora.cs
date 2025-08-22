using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataInfMicroVectorSaldoMora : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        public bool? PoseeSectorCooperativo { get; set; }
        public bool? PoseeSectorFinanciero { get; set; }
        public bool? PoseeSectorReal { get; set; }
        public bool? PoseeSectorTelcos { get; set; }
        public DateTime? Fecha { get; set; }
        public int? TotalCuentasMora { get; set; }
        public decimal? SaldoDeudaTotalMora { get; set; }
        public decimal? SaldoDeudaTotal { get; set; }
        [StringLength(30)]
        public string? MorasMaxSectorFinanciero { get; set; }
        [StringLength(30)]
        public string? MorasMaxSectorReal { get; set; }
        [StringLength(30)]
        public string? MorasMaxSectorTelcos { get; set; }
        [StringLength(30)]
        public string? MorasMaximas { get; set; }
        public int? NumCreditos30 { get; set; }
        public int? NumCreditosMayorIgual60 { get; set; }
        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
