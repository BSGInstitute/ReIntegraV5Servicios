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

        // ID de la cuenta Manager (MCC) para acceso API a sub-cuentas
        public string? ManagerAccountId { get; set; }

        // Conversiones Offline - IDs de acciones de conversión
        public string? ConversionActionIdIT { get; set; }

        public string? ConversionActionIdIPPF { get; set; }

        public string? ConversionActionIdICISM { get; set; }

        // Control del proceso
        public bool ProcesoConversionesActivo { get; set; } = true;

        public string ApiVersion { get; set; } = "v20";
    }
}
