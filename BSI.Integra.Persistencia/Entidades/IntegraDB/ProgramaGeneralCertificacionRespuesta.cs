using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralCertificacionRespuesta : BaseIntegraEntity
    {
        public int IdOportunidad { get; set; }
        public int IdProgramaGeneralCertificacion { get; set; }
        public int Respuesta { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
}
