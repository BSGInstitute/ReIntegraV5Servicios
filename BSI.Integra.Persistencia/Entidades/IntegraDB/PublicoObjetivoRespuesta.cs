using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PublicoObjetivoRespuesta : BaseIntegraEntity
    {
        public int IdOportunidad { get; set; }
        public int IdDocumentoSeccionPw { get; set; }
        public int NivelCumplimiento { get; set; }
    }
}
