using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class WhatsAppEstadoMensajeEnviado : BaseIntegraEntity
    {
        [StringLength(50)]
        public string? WaId { get; set; }
        [StringLength(50)]
        public string? WaRecipientId { get; set; }
        [StringLength(50)]
        public string? WaStatus { get; set; }
        [StringLength(11)]
        public string? WaTimeStamp { get; set; }
        public int IdPais { get; set; }
        public bool? EsMigracion { get; set; }
    }
}
