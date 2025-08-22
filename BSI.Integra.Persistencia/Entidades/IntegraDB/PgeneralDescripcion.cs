using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PgeneralDescripcion : BaseIntegraEntity
    {
        public int? IdPgeneral { get; set; }
        public string Texto { get; set; } = null!;
    }
}
