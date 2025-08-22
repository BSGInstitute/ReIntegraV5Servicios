using Org.BouncyCastle.Math;
using Org.BouncyCastle.Utilities;
using Renci.SshNet.Common;
using System.Numerics;
using System.Security;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MessengerChatDTO
    {
        public int Id { get; set; }
        public int? IdMeseengerUsuario { get; set; }
        public int? IdPersonal { get; set; }
        public string? Mensaje { get; set; }
        public bool? Tipo { get; set; }
        public string? FacebookId { get; set; }
        public DateTime? FechaInteraccion { get; set; }
        public int? IdTipoMensajeMessenger { get; set; }
        public string? UrlArchivoAdjunto { get; set; }
        public bool? Leido { get; set; }
        public DateTime? FechaLectura { get; set; }
        public int? IdFacebookAnuncio { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class MessengerChatHistorialDTO
    {
        public int Id { get; set; }
        public string PSID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public int? IdAlumno { get; set; }
        public string UrlFoto { get; set; }
        public int IdPersonal { get; set; }
        public bool SeRespondio { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string FacebookPaginaId { get; set; }
        public string Pagina { get; set; }
        public bool Desuscrito { get; set; }
        public bool TieneMasivo { get; set; }
        public int? IdMessengerChatMasivo { get; set; }
    }

    // Chat Messenger

    public class MessengerMensajeRecibidoDTO
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Is_echo { get; set; }
        public string? App_id { get; set; }
        public string? Type { get; set; }
        public string? Mid { get; set; }
        public string? Text { get; set; }
        public string? Url { get; set; }
        public bool? Estado { get; set; }
        public string? Usuario { get; set; }
    }

    public class MessengerMensajeEnviadoDTO
{
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Type { get; set; }
        public string Mid { get; set; }
        public string? Text { get; set; }
        public string? Url { get; set; }
        public bool? Estado { get; set; }
        public string? Usuario{ get; set; }
    }

    public class EventoMensajeMessengerDTO
    {
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string EventType { get; set; }
        public string Mids { get; set; }
        public bool Estado { get; set; }
        public string Usuario{ get; set; }

    }

}
