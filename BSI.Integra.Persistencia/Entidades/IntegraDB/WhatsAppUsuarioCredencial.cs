using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class WhatsAppUsuarioCredencial : BaseIntegraEntity
    {
        public int IdWhatsAppUsuario { get; set; }
        public int IdWhatsAppConfiguracion { get; set; }
        public string? UserAuthToken { get; set; }
        public DateTime? ExpiresAfter { get; set; }
        public bool? EsMigracion { get; set; }
    }
}
