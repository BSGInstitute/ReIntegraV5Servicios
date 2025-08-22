using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralPrerequisitoRespuesta : BaseIntegraEntity
    {
        public int IdOportunidad { get; set; }
        public int IdProgramaGeneralPrerequisito { get; set; }
        public int Respuesta { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
}
