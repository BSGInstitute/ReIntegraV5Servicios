using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DocumentoLegalAreaTrabajo : BaseIntegraEntity
    {
        public int IdDocumentoLegal { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
    }
}
