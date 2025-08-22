using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralProblemaModalidad : BaseIntegraEntity
    {
        public int IdProgramaGeneralProblema { get; set; }
        public int IdModalidadCurso { get; set; }
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        public int IdPgeneral { get; set; }
    }
}
