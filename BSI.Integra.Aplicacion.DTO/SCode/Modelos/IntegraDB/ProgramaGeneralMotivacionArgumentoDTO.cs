namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProgramaGeneralMotivacionArgumentoDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralMotivacion { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPgeneral { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ProgramaGeneralMotivacionArgumentoComboDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralMotivacion { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
