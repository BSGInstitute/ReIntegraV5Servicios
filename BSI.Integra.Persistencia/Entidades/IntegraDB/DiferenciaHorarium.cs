using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DiferenciaHorarium : BaseIntegraEntity
    {
        public int IdPaisOrigen { get; set; }
        public int IdPaisDestino { get; set; }
        public int DiferenciaHoraria { get; set; }
        public int? IdMigracion { get; set; }
    }
}
