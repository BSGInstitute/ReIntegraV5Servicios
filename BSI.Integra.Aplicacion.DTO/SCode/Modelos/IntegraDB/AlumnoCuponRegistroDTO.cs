namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AlumnoCuponRegistroDTO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public string CodigoCupon { get; set; } = null!;
        public int? IdPersonal { get; set; }
        public string? AreaVentas { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class AlumnoCuponRegistroComboDTO
    {
        public int Id { get; set; }
        public string CodigoCupon { get; set; } = null!;
    }
}
