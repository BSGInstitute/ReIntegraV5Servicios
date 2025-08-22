using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PgeneralProyectoAplicacionProveedor : BaseIntegraEntity
    {
        public int IdPgeneralProyectoAplicacion { get; set; }
        public int IdProveedor { get; set; }
    }
}
