using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class MontoPagoCronogramaDetalle : BaseIntegraEntity
    {
        public int NumeroCuota { get; set; }
        public double MontoCuota { get; set; }
        public DateTime FechaPago { get; set; }
        [StringLength(100)]
        public string CuotaDescripcion { get; set; } = null!;
        public double MontoCuotaDescuento { get; set; }
        public bool Pagado { get; set; }
        public int? IdMontoPagoCronograma { get; set; }
        public bool Matricula { get; set; }
    }
}
