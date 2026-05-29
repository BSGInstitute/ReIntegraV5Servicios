using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TipoSubTipoEncuesta : BaseIntegraEntity
    {
        public int IdTipoEncuesta { get; set; }
        public int IdSubTipoEncuesta { get; set; }
    }
}
