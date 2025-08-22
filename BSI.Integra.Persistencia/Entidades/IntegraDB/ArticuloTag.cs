using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ArticuloTag : BaseIntegraEntity
    {
        public int IdArticulo { get; set; }
        public int IdTag { get; set; }
    }
}
