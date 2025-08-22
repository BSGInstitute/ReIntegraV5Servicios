using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PgeneralRelacionado : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public int IdPgeneralRelacionado { get; set; }
    }
}
