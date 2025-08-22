using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ChatIntegraHistorialAsesor : BaseIntegraEntity
    {
        public int IdAsesorChatDetalle { get; set; }
        public int? IdPersonal { get; set; }
        public DateTime FechaAsignacion { get; set; }
    }
}
