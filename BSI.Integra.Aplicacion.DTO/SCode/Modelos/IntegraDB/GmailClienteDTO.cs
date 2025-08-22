namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class GmailClienteDTO
    {
        public int Id { get; set; }
        public int? IdAsesor { get; set; }
        public string EmailAsesor { get; set; } = null!;
        public string PasswordCorreo { get; set; } = null!;
        public string NombreAsesor { get; set; } = null!;
        public string IdClient { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
        public string? AliasEmailAsesor { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class GmailClienteComboDTO
    {
        public int Id { get; set; }
        public string NombreAsesor { get; set; } = null!;
    }
    public class CorreoClienteCredencialDTO
    {
        public int? IdAsesor { get; set; }
        public string EmailAsesor { get; set; } = null!;
        public string PasswordCorreo { get; set; } = null!;
    }
}
