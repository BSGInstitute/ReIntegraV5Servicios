using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SentinelSdtResVenItem : BaseIntegraEntity
    {
        public int? IdSentinel { get; set; }
        [StringLength(20)]
        public string? TipoDocumento { get; set; }
        [StringLength(20)]
        public string? NroDocumento { get; set; }
        public int? CantidadDocs { get; set; }
        [StringLength(200)]
        public string? Fuente { get; set; }
        [StringLength(200)]
        public string? Entidad { get; set; }
        public decimal? Monto { get; set; }
        public short? Cantidad { get; set; }
        public int? DiasVencidos { get; set; }
    }
}
