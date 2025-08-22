namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class WhatsAppUsuarioDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string RolUser { get; set; }
        public string UserUsername { get; set; }
        public string UserPassword { get; set; }
        public bool? EsMigracion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int? IdMigracion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }

    public class userLogeo
    {
        public users[] users { get; set; }
        public meta meta { get; set; }
        public errors[] errors { get; set; }
    }

    public class users
    {
        public string username { get; set; }
        public string token { get; set; }
        public string expires_after { get; set; }
    }

    public class meta
    {
        public string version { get; set; }
        public string api_status { get; set; }
    }

    public class errors
    {
        public string code { get; set; }
        public string title { get; set; }
        public string details { get; set; }
    }

    public class WhatsAppPersonalDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Rol { get; set; }
        public string UserName { get; set; }
    }

    public class WhatsAppDatoUsuarioDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string RolUser { get; set; }
        public string UserUsername { get; set; }
        public string UserPassword { get; set; }
        public string UsuarioSistema { get; set; }
    }

    public class WhatsAppUsuarioListaGrillaDTO
    {
        public int IdWhatsAppUsuario { get; set; }
        public int IdPersonal { get; set; }
        public string RolUser { get; set; }
        public string UserUsername { get; set; }
        public string UserPassword { get; set; }
        public string Nombres { get; set; }
    }

    public class WhatsAppUsuarioEntradaDTO
    {
        public int? Id { get; set; }
        public int IdPersonal { get; set; }
        public string RolUser { get; set; }
        public string UserUsername { get; set; }
        public string UserPassword { get; set; }
        public string UsuarioSistema { get; set; }
    }
}
