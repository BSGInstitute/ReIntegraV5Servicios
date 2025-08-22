namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PespecificoFrecuenciaDetalleDTO
    {
        public int? Id { get; set; }
        public int? IdPespecificoFrecuencia { get; set; }
        public byte DiaSemana { get; set; }
        public TimeSpan HoraDia { get; set; }
        public decimal Duracion { get; set; }
    }
}
