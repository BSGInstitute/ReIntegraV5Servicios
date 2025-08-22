using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class WhatsAppMensajeRecibido : BaseIntegraEntity
    {
        [StringLength(50)]
        public string WaFrom { get; set; }
        [StringLength(250)]
        public string WaId { get; set; }
        [StringLength(1)]
        public string WaTimeStamp { get; set; }
        [StringLength(50)]
        public string WaType { get; set; }
        public int? WaTypeMensaje { get; set; }
        [StringLength(50)]
        public string WaIdTypeMensaje { get; set; }
        public string WaBody { get; set; }
        public string WaFile { get; set; }
        [StringLength(100)]
        public string WaFileName { get; set; }
        [StringLength(100)]
        public string WaMimeType { get; set; }
        public string WaSha256 { get; set; }
        public string WaCaption { get; set; }
        public int IdPais { get; set; }
        public int IdPersonal { get; set; }
        public int IdAlumno { get; set; }
        public bool? EsMigracion { get; set; }
        public int? IdMigracion { get; set; }
        public bool? MensajeOfensivo { get; set; }
    }
}
