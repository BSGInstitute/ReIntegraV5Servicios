using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class GestionPago : BaseIntegraEntity
    {
        public int IdComprobantePago { get; set; }
        public bool? ServicioValidado { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public string? ObservacionDocumentacion { get; set; }
        public string? LevantamientoObservacion { get; set; }
        public bool? ConformidadFinanzas { get; set; }
        public string? ObservacionProgramacionPago { get; set; }
        public int? IdModalidadPago { get; set; }
        public int? IdPagoEstado { get; set; }
    }
}
