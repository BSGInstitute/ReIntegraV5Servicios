using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PgeneralForoAsignacionProveedor : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public int IdModalidadCurso { get; set; }
        public int IdProveedor { get; set; }
        public int? IdMigracion { get; set; }
    }
}
