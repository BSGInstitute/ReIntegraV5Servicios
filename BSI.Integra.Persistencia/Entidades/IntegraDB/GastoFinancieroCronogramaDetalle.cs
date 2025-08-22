using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class GastoFinancieroCronogramaDetalle : BaseIntegraEntity
    {
        public int IdGastoFinancieroCronograma { get; set; }
        public int NumeroCuota { get; set; }
        public decimal CapitalCuota { get; set; }
        public decimal InteresCuota { get; set; }
        public DateTime FechaVencimientoCuota { get; set; }
    }
}
