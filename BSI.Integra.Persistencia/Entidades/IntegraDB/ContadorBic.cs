using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ContadorBic : BaseIntegraEntity
    {
        public int IdOportunidad { get; set; }
        public int DiasSinContactoManhana { get; set; }
        public int DiasSinContactoTarde { get; set; }
        public int? IdMigracion { get; set; }
    }
}
