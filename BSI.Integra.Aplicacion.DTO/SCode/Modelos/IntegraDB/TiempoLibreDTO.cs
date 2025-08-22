namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TiempoLibreDTO
    {
        public int Id { get; set; }
        public int TiempoMin { get; set; }
        public int Tipo { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class TiempoLibreRADTO
    {
        public int TiempoMin { get; set; }
    }
}
