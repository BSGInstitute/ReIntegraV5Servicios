using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PgeneralAsubPgeneralVersionPrograma : BaseIntegraEntity
    {
        public int IdPgeneralAsubPgeneral { get; set; }
        public int? IdVersionPrograma { get; set; }
        public int? IdMigracion { get; set; }
    }
}
