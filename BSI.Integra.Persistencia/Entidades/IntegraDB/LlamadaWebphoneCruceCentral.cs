using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class LlamadaWebphoneCruceCentral : BaseIntegraEntity
    {
        public int IdLlamadaWebphone { get; set; }
        public int IdLlamadaCentral { get; set; }
        public DateTime FechaIncioLlamadaWebphone { get; set; }
        public DateTime FechaFinLlamadaWebphone { get; set; }
        public DateTime FechaIncioLlamadaCentral { get; set; }
        public DateTime FechaFinLlamadaCentral { get; set; }
        public string AnexoWebphone { get; set; } = null!;
        public string AnexoCentral { get; set; } = null!;
        public int DuracionTimbradoWebPhone { get; set; }
        public int DuracionContestoWebPhone { get; set; }
        public int DuracionTimbradoCentral { get; set; }
        public int DuracionContestoCentral { get; set; }
        public int IdAlumno { get; set; }
        public int IdActividadDetalle { get; set; }
        public string TelefonoDestinoWebPhone { get; set; } = null!;
        public string TelefonoDestinoCentral { get; set; } = null!;
        public int IdLlamadaWebPhoneEstado { get; set; }
        public string EstadoLlamadaCentral { get; set; } = null!;
        public string SubEstadoLlamadaCentral { get; set; } = null!;
        public string? UrlAudio { get; set; }
        public string? Troncal { get; set; }
    }
}
