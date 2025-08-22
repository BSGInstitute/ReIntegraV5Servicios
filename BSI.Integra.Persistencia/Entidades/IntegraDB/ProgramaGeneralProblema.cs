using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralProblema : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        [StringLength(400)]
        public string Nombre { get; set; } = null!;
        public bool EsVisibleAgenda { get; set; }
        public List<ProgramaGeneralProblemaDetalleSolucion> ProgramaGeneralProblemaDetalleSolucion { get; set; } = new List<ProgramaGeneralProblemaDetalleSolucion>();
        public List<ProgramaGeneralProblemaModalidad> ProgramaGeneralProblemaModalidad { get; set; } = new List<ProgramaGeneralProblemaModalidad>();
    }
}
