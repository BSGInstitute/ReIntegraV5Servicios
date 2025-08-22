using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class NotaIngresoCaja : BaseIntegraEntity
    {
        [Required]
        public int IdCaja { get; set; }
        [Required]
        [StringLength(50)]
        public string CodigoNic { get; set; } = null!;
        [Required]
        public int IdOrigenIngresoCaja { get; set; }
        [Required]
        public int IdPersonalEmitido { get; set; }
        [Required]
        public decimal Monto { get; set; }
        [Required]
        public DateTime FechaGiro { get; set; }
        [Required]
        [StringLength(200)]
        public string Concepto { get; set; } = null!;
        [Required]
        public DateTime FechaCobro { get; set; }
        [Required]
        public int Anho { get; set; }
    }
}
