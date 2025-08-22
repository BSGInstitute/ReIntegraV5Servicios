using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralBeneficioModalidad : BaseIntegraEntity
    {
        public int IdProgramaGeneralBeneficio { get; set; }
        public int IdModalidadCurso { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPgeneral { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
