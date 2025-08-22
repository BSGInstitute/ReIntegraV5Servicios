namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class Llamada3CXDTO
    {
        public int? Id { get; set; }
        public DateTime? FechaInicioLlamada { get; set; }
        public DateTime? FechaFinLlamada { get; set; }
        public string? TiempoDuracion { get; set; }
        public string? EstadoLlamada { get; set; }
        public string? SubEstadoLlamada { get; set; }
        public string? NombreGrabacion { get; set; }
    }
}
