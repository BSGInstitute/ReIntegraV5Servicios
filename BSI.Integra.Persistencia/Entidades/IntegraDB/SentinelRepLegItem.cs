using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SentinelRepLegItem : BaseIntegraEntity
    {
        public int? IdSentinel { get; set; }
        [StringLength(2)]
        public string TipoDocumento { get; set; } = null!;
        [StringLength(15)]
        public string NumeroDocumento { get; set; } = null!;
        [StringLength(100)]
        public string? Nombres { get; set; }
        [StringLength(100)]
        public string? ApellidoPaterno { get; set; }
        [StringLength(100)]
        public string? ApellidoMaterno { get; set; }
        [StringLength(200)]
        public string? RazonSocial { get; set; }
        [StringLength(200)]
        public string? Cargo { get; set; }
        [StringLength(200)]
        public string SemaforoActual { get; set; } = null!;
    }
}
