using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralProblemaDetalleSolucion : BaseIntegraEntity
    {
        public int IdProgramaGeneralProblema { get; set; }
        public string Detalle { get; set; } = null!;
        public string Solucion { get; set; } = null!;
        public int IdPgeneral { get; set; }
    }
}
