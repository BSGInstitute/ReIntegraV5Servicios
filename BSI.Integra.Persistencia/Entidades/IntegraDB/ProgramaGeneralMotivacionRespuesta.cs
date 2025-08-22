using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralMotivacionRespuesta : BaseIntegraEntity
    {
        public int IdOportunidad { get; set; }
        public int IdProgramaGeneralMotivacion { get; set; }
        public int Respuesta { get; set; }
    }
}
