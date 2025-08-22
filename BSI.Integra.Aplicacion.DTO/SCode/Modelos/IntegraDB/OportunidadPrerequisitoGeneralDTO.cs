namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class OportunidadPrerequisitoGeneralDTO
    {
        public int IdOportunidadCompetidor { get; set; }
        public int IdProgramaGeneralBeneficio { get; set; }
        public int Respuesta { get; set; }
        public string Completado { get; set; }
    }
    public class OportunidadPrerequisitoGeneralAlternoDTO
    {
        public int IdOportunidad { get; set; }
        public int IdProgramaGeneralPrerequisito { get; set; }
        public int Respuesta { get; set; }
    }
}
