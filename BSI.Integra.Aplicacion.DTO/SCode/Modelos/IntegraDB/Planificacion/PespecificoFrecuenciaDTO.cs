namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PespecificoFrecuenciaDTO
    {
        public int? Id { get; set; }
        public int? IdPespecifico { get; set; }
        public DateTime FechaInicio { get; set; }
        public int Frecuencia { get; set; }
        public int NroSesiones { get; set; }
        public int? IdFrecuencia { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
