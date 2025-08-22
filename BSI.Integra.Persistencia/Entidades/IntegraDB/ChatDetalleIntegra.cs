using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ChatDetalleIntegra : BaseIntegraEntity
    {
        public int? IdInteraccionChatIntegra { get; set; }
        [StringLength(200)]
        public string? NombreRemitente { get; set; }
        [StringLength(100)]
        public string IdRemitente { get; set; } = null!;
        [StringLength(1000)]
        public string? Mensaje { get; set; }
        public DateTime Fecha { get; set; }
        public int NroMensajesSinLeer { get; set; }
        public bool? MensajeOfensivo { get; set; }
        public int? IdChatDetalleIntegraArchivo { get; set; }
    }
}
