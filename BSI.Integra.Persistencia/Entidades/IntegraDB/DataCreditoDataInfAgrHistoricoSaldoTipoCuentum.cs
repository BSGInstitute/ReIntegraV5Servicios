using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataInfAgrHistoricoSaldoTipoCuentum : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        [StringLength(20)]
        public string? Tipo { get; set; }
        public DateTime? Fecha { get; set; }
        public int? TotalCuentas { get; set; }
        public int? CuentasConsideradas { get; set; }
        public decimal? Saldo { get; set; }

        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
