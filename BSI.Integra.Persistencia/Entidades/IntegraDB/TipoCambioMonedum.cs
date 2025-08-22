using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TipoCambioMonedum : BaseIntegraEntity
    {
        public double MonedaAdolar { get; set; }
        public double DolarAmoneda { get; set; }
        public DateTime Fecha { get; set; }
        public int IdMoneda { get; set; }
        public int? IdTipoCambioCol { get; set; }
        public int? IdTipoCambio { get; set; }
    }
}
