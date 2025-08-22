using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TipoCambio : BaseIntegraEntity
    {
        public double SolesDolares { get; set; }
        public double DolaresSoles { get; set; }
        public DateTime Fecha { get; set; }
    }
}
