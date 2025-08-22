using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FurPago : BaseIntegraEntity
    {
        public int? IdFur { get; set; }
        public int? IdComprobantePago { get; set; }
        public int NumeroPago { get; set; }
        public int IdMoneda { get; set; }
        public string NumeroRecibo { get; set; } = null!;
        public int IdFormaPago { get; set; }
        public DateTime FechaCobroBanco { get; set; }
        public decimal PrecioTotalMonedaOrigen { get; set; }
        public decimal PrecioTotalMonedaDolares { get; set; }
        public int IdCuentaCorriente { get; set; }
        public int? IdComprobantePagoPorFur { get; set; }
    }
}
