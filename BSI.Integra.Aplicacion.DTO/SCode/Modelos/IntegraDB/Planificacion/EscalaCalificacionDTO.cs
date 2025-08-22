namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class EscalaCalificacionDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public IEnumerable<EscalaCalificacionDetalleDTO>? EscalaCalificacionDetalles { get; set; }
    }
}

