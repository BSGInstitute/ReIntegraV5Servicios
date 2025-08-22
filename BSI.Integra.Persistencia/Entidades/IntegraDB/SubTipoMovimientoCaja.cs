using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SubTipoMovimientoCaja : BaseIntegraEntity
    {

        public int IdTipoMovimientoCaja { get; set; }
     
        public string Nombre { get; set; } = null!;

        public Guid? IdMigracion { get; set; }
    }
}
