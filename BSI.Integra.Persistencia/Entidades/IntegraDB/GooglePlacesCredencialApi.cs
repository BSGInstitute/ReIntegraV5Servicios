using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class GooglePlacesCredencialApi : BaseIntegraEntity
    {
        [StringLength(150)]
        public string NombreServicio { get; set; } = null!;

        [StringLength(500)]
        public string ApiKey { get; set; } = null!;

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [StringLength(500)]
        public string? Oauth2ClientId { get; set; }

        [StringLength(500)]
        public string? Oauth2ClientSecret { get; set; }

        [StringLength(1000)]
        public string? Oauth2RefreshToken { get; set; }
    }
}
