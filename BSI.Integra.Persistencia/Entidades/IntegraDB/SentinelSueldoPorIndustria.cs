using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SentinelSueldoPorIndustria : BaseIntegraEntity
    {
        public int? IdCargo { get; set; }
        public int? IdIndustria { get; set; }
        public int? Tipo { get; set; }
    }
}
