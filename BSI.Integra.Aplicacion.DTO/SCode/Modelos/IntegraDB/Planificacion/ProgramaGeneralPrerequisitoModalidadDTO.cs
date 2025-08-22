namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ProgramaGeneralPrerequisitoModalidadDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralPrerequisito { get; set; }
        public int IdModalidadCurso { get; set; }
        public string Nombre { get; set; }
        public int IdPgeneral { get; set; }
    }
    public class CompuestoPreRequisitoModalidadDTO
    {
        public int IdPreRequisito { get; set; }
        public int IdPGeneral { get; set; }
        public string NombrePreRequisito { get; set; }
        public int Orden { get; set; }
        public int Tipo { get; set; }
        public List<ModalidadCursoProblemaDTO> Modalidades { get; set; }
    }
    public class PreRequisitoModalidadDTO
    {
        public int IdPreRequisito { get; set; }
        public int IdPGeneral { get; set; }
        public string NombrePreRequisito { get; set; }
        public int Orden { get; set; }
        public int Tipo { get; set; }
        public int IdModalidadCurso { get; set; }
        public string NombreModalidad { get; set; }
        public int IdModalidadPreRequisito { get; set; }
    }
}