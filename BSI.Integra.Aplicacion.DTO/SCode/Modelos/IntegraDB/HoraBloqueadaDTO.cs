namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class HoraBloqueadaDTO
    {
        public int Id { get; set; }
        public int? IdPersonal { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class HoraBloqueadaRADTO
    {
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
    }
}
