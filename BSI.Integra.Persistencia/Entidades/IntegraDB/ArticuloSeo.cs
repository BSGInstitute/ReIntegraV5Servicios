using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ArticuloSeo : BaseIntegraEntity
    {
        public string Descripcion { get; set; } = null!;
        public int IdArticulo { get; set; }
        public int IdParametroSeo { get; set; }
    }
}
