using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class AdworkCredencialApi : BaseIntegraEntity
    {
        public string DeveloperToken { get; set; } = null!;

        public string ClientCustomerId { get; set; } = null!;

        public string Oauth2ClientId { get; set; } = null!;

        public string Oauth2ClientSecret { get; set; } = null!;

        public string Oauth2RefreshToken { get; set; } = null!;
    }
}
