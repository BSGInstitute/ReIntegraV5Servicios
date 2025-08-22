using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class GmailCorreo : BaseIntegraEntity
    {
        public int? IdEtiqueta { get; set; }
        public int? IdGmailCliente { get; set; }
        [StringLength(50)]
        public string? IdCorreoGmailFormat { get; set; }
        [StringLength(500)]
        public string? Asunto { get; set; }
        public DateTime? Fecha { get; set; }
        public string? EmailBody { get; set; }
        public bool? Seen { get; set; }
        [StringLength(300)]
        public string? Remitente { get; set; }
        public string? Destinatarios { get; set; }
        public int? IdPersonal { get; set; }
        public int? Filas { get; set; }
        public int? IdInteraccion { get; set; }
        public string? Cc { get; set; }
        [StringLength(500)]
        public string? ResumenMensaje { get; set; }
        [StringLength(2000)]
        public string? Bcc { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public List<GmailCorreoArchivoAdjunto> GmailCorreoArchivoAdjuntos { get; set; }
    }
}
