using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Periodo : BaseIntegraEntity
    {
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaInicialFinanzas { get; set; }
        public DateTime FechaFinFinanzas { get; set; }
        public DateTime? FechaInicialRepIngresos { get; set; }
        public DateTime? FechaFinRepIngresos { get; set; }
    }
}
