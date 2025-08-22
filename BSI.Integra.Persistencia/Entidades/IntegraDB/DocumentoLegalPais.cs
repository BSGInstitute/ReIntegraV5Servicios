using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DocumentoLegalPais : BaseIntegraEntity
    {
        public int IdDocumentoLegal { get; set; }
        public int IdPais { get; set; }
    }
}
