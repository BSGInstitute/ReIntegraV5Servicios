using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class MessengerChat : BaseIntegraEntity
    {
        public int? IdMeseengerUsuario { get; set; }
        public int? IdPersonal { get; set; }
        public string? Mensaje { get; set; }
        public bool? Tipo { get; set; }
        [StringLength(160)]
        public string? FacebookId { get; set; }
        public DateTime? FechaInteraccion { get; set; }
        public int? IdTipoMensajeMessenger { get; set; }
        [StringLength(800)]
        public string? UrlArchivoAdjunto { get; set; }
        public bool? Leido { get; set; }
        public DateTime? FechaLectura { get; set; }
        public int? IdFacebookAnuncio { get; set; }
    }
}
