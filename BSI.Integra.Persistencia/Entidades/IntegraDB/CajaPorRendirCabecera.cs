using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CajaPorRendirCabecera : BaseIntegraEntity
    {
        [Required]
        public int IdCaja { get; set; }
        [Required]
        [StringLength(50)]
        public string Codigo { get; set; } = null!;
        [Required]
        public int Anho { get; set; }
        [Required]
        public int IdPersonalAprobacion { get; set; }
        [Required]
        public int IdPersonalSolicitante { get; set; }
        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Observacion { get; set; } = null!;
        [Required]
        public bool EsRendido { get; set; }
        [Required]
        public decimal MontoDevolucion { get; set; }
    }
}
