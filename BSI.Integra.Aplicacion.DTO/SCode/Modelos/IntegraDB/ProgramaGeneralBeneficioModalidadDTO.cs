using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProgramaGeneralBeneficioModalidadDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralBeneficio { get; set; }
        public int IdModalidadCurso { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPgeneral { get; set; }
    }
    public class CompuestoBeneficioModalidadAlternoDTO
    {
        public int IdBeneficio { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreBeneficio { get; set; }
        public List<ComboDTO> BeneficiosArgumentos { get; set; }
        public List<ModalidadCursoAlternoDTO> Modalidades { get; set; }
    }
    public class BeneficioModalidadDTO
    {
        public int IdBeneficio { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreBeneficio { get; set; }
        public int IdModalidadCurso { get; set; }
        public string NombreModalidad { get; set; }
        public int? IdArgumentoBeneficio { get; set; }
        public string NombreArgumentoBeneficio { get; set; }
        public int IdModalidadBeneficio { get; set; }
    }
}
