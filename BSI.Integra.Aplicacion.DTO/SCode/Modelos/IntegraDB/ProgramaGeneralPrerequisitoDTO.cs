using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProgramaGeneralPrerequisitoDTO
    {
        public int? Id { get; set; }
        public int? IdPgeneral { get; set; }
        public string Nombre { get; set; } = null!;
        public int Tipo { get; set; }
        public int? Orden { get; set; }
    }
    public class ProgramaGeneralPrerequisitoComboDTO
    {
        public int Id { get; set; }
        public int? IdPgeneral { get; set; }
        public string Nombre { get; set; } = null!;
    }
    public class ProgramaGeneralPrerequisitoOportunidadDTO
    {
        public int IdPrerequisito { get; set; }
        public string PRNombre { get; set; } = null!;
        public int Respuesta { get; set; }
        public string Completado { get; set; } = null!;
    }
}
