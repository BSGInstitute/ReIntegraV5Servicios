using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralBeneficioRespuesta : BaseIntegraEntity
    {
        public int IdOportunidad { get; set; }
        public int IdProgramaGeneralBeneficio { get; set; }
        public int Respuesta { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
}
