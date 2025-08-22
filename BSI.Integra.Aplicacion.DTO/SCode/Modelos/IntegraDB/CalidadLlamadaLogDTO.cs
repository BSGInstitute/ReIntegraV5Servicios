namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CalidadLlamadaLogDTO
    {
        public int Id { get; set; }
        public int IdProblemaLlamada { get; set; }
        public int IdCalidadLlamada { get; set; }
        public int? IdActividadDetalle { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
