using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataInfAgrHistoricoSaldoTotal : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        public DateTime? Fecha { get; set; }
        public int? TotalCuentas { get; set; }
        public int? CuentasConsideradas { get; set; }
        public decimal? Saldo { get; set; }

        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
