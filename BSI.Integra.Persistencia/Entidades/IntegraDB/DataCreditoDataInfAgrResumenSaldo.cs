using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataInfAgrResumenSaldo : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        public decimal? SaldoTotalEnMora { get; set; }
        public decimal? SaldoM30 { get; set; }
        public decimal? SaldoM60 { get; set; }
        public decimal? SaldoM90 { get; set; }
        public decimal? CuotaMensual { get; set; }
        public decimal? SaldoCreditoMasAlto { get; set; }
        public decimal? SaldoTotal { get; set; }
        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
