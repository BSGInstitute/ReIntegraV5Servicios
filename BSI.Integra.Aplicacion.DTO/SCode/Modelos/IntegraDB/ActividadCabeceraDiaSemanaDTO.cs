namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ActividadCabeceraDiaSemanaDTO
    {
        public int Id { get; set; }
        public int IdActividadCabecera { get; set; }
        public int IdDiaSemana { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }

}
