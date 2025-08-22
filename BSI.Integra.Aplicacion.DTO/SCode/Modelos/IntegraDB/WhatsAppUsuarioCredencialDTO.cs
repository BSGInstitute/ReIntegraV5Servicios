namespace BSI.Integra.Aplicacion.Modelos.IntegraDB
{

    public class WhatsAppUsuarioCredencialDTO
    {
        public int Id { get; set; }
        public int IdWhatsAppUsuario { get; set; }
        public int IdWhatsAppConfiguracion { get; set; }
        public string UserAuthToken { get; set; }
        public DateTime? ExpiresAfter { get; set; }
        public bool? EsMigracion { get; set; }
        public int? IdMigracion { get; set; }
    }
    public class CredencialTokenExpiraDTO
    {
        public int IdWhatsAppUsuario { get; set; }
        public string UserAuthToken { get; set; }
        public DateTime ExpiresAfter { get; set; }
    }
    public class CredencialUsuarioLoginDTO
    {
        public int IdWhatsAppUsuario { get; set; }
        public string UserUsername { get; set; }
        public string UserPassword { get; set; }
    }
    public class UsuarioRegister
    {
        public string UserUsername { get; set; }
        public string UserPassword { get; set; }
    }
}
