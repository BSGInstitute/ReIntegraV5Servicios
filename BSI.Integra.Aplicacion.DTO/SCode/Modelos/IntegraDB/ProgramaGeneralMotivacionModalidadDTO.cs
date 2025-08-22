namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProgramaGeneralMotivacionModalidadDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralMotivacion { get; set; }
        public int IdModalidadCurso { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPgeneral { get; set; }
        public int? IdMigracion { get; set; }
    }
    public class CompuestoMotivacionModalidadAlternoDTO
    {
        public int IdMotivacion { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreMotivacion { get; set; }
        public List<ComboDTO> MotivacionesArgumentos { get; set; }
        public List<ModalidadCursoAlternoDTO> Modalidades { get; set; }
    }
}
