using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public partial class PgeneralProyectoAplicacion : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public List<PgeneralProyectoAplicacionModalidad> PgeneralProyectoAplicacionModalidad { get; set; }
        public List<PgeneralProyectoAplicacionProveedor> PgeneralProyectoAplicacionProveedor { get; set; }
    }
}
