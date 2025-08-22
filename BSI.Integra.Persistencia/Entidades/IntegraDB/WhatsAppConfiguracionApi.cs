using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class WhatsAppConfiguracionApi : BaseIntegraEntity
    {
        public string? Numero { get; set; }
        public string? VName { get; set; }
        public int? IdPais { get; set; }
        public string? Bearer { get; set; }
        public string? NumeroIndentificador { get; set; }
        public string? VersionApi { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public bool? EsMigracion { get; set; }
        public int? IdMigracion { get; set; }
        public string? UserUsername { get; set; }
        public string? UserPassword { get; set; }
        public string? NumeroWhatsApp { get; set;}
        public string? CuentaIdentificadorWhatsApp { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public string? CodigoArea { get; set; }
        public int? IdPersonalAsignado { get; set; }
    }
}