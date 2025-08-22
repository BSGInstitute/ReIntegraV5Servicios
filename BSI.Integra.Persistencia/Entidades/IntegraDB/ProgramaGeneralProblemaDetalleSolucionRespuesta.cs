using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralProblemaDetalleSolucionRespuesta : BaseIntegraEntity
    {
        public int IdOportunidad { get; set; }
        public int IdProgramaGeneralProblemaDetalleSolucion { get; set; }
        public bool EsSeleccionado { get; set; }
        public bool EsSolucionado { get; set; }
    }
}
