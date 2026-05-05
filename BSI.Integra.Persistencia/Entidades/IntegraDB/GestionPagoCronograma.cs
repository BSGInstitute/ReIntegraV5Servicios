using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class GestionPagoCronograma : BaseIntegraEntity
    {
        public int IdGestionPago { get; set; }
        public int NumeroCuota { get; set; }
        public decimal MontoCuota { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaProbablePago { get; set; }
        public DateTime? FechaRealPago { get; set; }
    }
}
