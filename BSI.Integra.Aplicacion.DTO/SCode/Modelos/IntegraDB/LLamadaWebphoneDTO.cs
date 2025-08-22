namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class LlamadaIntegraDTO
    {
        public int? Id { get; set; }
        public DateTime? FechaInicioLlamada { get; set; }
        public DateTime? FechaFinLlamada { get; set; }
        public string? TiempoDuracion { get; set; }
        public string? TiempoDuracionMinutos { get; set; }
        public string? EstadoLlamada { get; set; }
        public string? SubEstadoLlamada { get; set; }
        public string? NombreGrabacion { get; set; }
        public string? Webphone { get; set; }
    }
    public class LlamadaIntegra3cxDTO
    {
        public int? Id { get; set; }
        public int? DuracionTimbrado { get; set; }
        public int? DuracionContesto { get; set; }
        public string? DuracionTimbradoMinutos { get; set; }
        public string? DuracionContestoMinutos { get; set; }
        public int? IdLlamadaCentral { get; set; }
        public DateTime? FechaInicioLlamada { get; set; }
        public DateTime? FechaFinLlamada { get; set; }
        public string? EstadoLlamada { get; set; }
        public string? SubEstadoLlamada { get; set; }
        public string? UrlGrabacion { get; set; }
        public string? NombreGrabacion { get; set; }
        public string? Webphone { get; set; }
        public string OrigenLlamada { get; set; }
    }
}
