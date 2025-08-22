using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ChatDetalleIntegraArchivo : BaseIntegraEntity
    {
        [StringLength(255)]
        public string? NombreArchivo { get; set; }
        [StringLength(256)]
        public string? RutaArchivo { get; set; }
        [StringLength(250)]
        public string? MimeType { get; set; }
        public bool? EsImagen { get; set; }
    }
}
