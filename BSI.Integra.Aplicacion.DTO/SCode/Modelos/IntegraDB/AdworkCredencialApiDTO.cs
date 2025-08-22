namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AdworkCredencialApiDTO
    {
        public int Id { get; set; }

        public string DeveloperToken { get; set; } = null!;

        public string ClientCustomerId { get; set; } = null!;

        public string Oauth2ClientId { get; set; } = null!;

        public string Oauth2ClientSecret { get; set; } = null!;

        public string Oauth2RefreshToken { get; set; } = null!;

        public bool Estado { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public string UsuarioModificacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        public int? IdMigracion { get; set; }
    }


}
