using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class WhatsAppConfiguracion : BaseIntegraEntity
    {
        public string? UrlWhatsApp { get; set; }
        [StringLength(150)]
        public string? IpHost { get; set; }
        [StringLength(100)]
        public string? Numero { get; set; }
        [StringLength(500)]
        public string? Vname { get; set; }
        public string? Certificado { get; set; }
        public int IdPais { get; set; }
        public bool? EsMigracion { get; set; }

    }
}
