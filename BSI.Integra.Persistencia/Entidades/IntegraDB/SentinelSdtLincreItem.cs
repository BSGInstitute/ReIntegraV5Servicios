using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SentinelSdtLincreItem : BaseIntegraEntity
    {
        public int? IdSentinel { get; set; }
        [StringLength(20)]
        public string? TipoDocumento { get; set; }
        [StringLength(20)]
        public string? NumeroDocumento { get; set; }
        [StringLength(50)]
        public string? CnsEntNomRazLn { get; set; }
        [StringLength(20)]
        public string? TipoCuenta { get; set; }
        public decimal? LineaCredito { get; set; }
        public decimal? LineaCreditoNoUtil { get; set; }
        public decimal? LineaUtil { get; set; }
    }
}
