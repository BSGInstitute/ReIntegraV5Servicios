using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CajaPorRendir : BaseIntegraEntity
    {
        public int? IdCaja { get; set; }
        public int? IdCajaPorRendirCabecera { get; set; }
        public int? IdFur { get; set; }
        [Required]
        public int IdPersonalSolicitante { get; set; }
        [Required]
        public int IdPersonalResponsableCaja { get; set; }
        [Required]
        [StringLength(300)]
        public string Descripcion { get; set; } = null!;
        [Required]
        public int IdMoneda { get; set; }
        [Required]
        public decimal TotalEfectivo { get; set; }
        [Required]
        public DateTime FechaEntregaEfectivo { get; set; }
        [Required]
        public bool EsEnviado { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? FechaAprobacion { get; set; }
    }
}
