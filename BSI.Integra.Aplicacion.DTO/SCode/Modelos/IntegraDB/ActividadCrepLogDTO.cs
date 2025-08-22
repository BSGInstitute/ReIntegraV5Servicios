namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ActividadCrepLogDTO
    {
        public int Id { get; set; }

        public string TipoOperacion { get; set; } = null!;

        public string TipoActividad { get; set; } = null!;

        public int EstadoOperacion { get; set; }

        public string? ExcepcionProceso { get; set; }

        public string Crep { get; set; } = null!;

    }

}

