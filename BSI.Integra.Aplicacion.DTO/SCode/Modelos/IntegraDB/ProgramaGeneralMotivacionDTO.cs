namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProgramaGeneralMotivacionDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; } = null!;
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ProgramaGeneralMotivacionComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
    public class ProgramaGeneralMotivacionAgendaDTO
    {
        public int IdMotivacion { get; set; }
        public string? NombreMotivacion { get; set; }
        public int Respuesta { get; set; }
        public string? Completado { get; set; }
    }
    public class ProgramaGeneralMotivacionDetalleAgendaDTO
    {
        public int IdMotivacion { get; set; }
        public string? NombreMotivacion { get; set; }
        public int Respuesta { get; set; }
        public string? Completado { get; set; }
        public List<ProgramaGeneralMotivacionArgumentoComboDTO> Argumentos { get; set; } = new List<ProgramaGeneralMotivacionArgumentoComboDTO>();
    }
    public class MotivacionModalidadDTO
    {
        public int IdMotivacion { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreMotivacion { get; set; }
        public int IdModalidadCurso { get; set; }
        public string NombreModalidad { get; set; }
        public int? IdArgumentoMotivacion { get; set; }
        public string NombreArgumentoMotivacion { get; set; }
        public int IdModalidadMotivacion { get; set; }
    }
}
