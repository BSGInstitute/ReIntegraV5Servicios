using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SentinelSdtRepSbsitem : BaseIntegraEntity
    {
        public int? IdSentinel { get; set; }
        [StringLength(20)]
        public string? TipoDoc { get; set; }
        [StringLength(20)]
        public string? NroDoc { get; set; }
        [StringLength(500)]
        public string? NombreRazonSocial { get; set; }
        [StringLength(20)]
        public string? Calificacion { get; set; }
        public decimal? MontoDeuda { get; set; }
        public int? DiasVencidos { get; set; }
        public DateTime? FechaReporte { get; set; }
    }
}
