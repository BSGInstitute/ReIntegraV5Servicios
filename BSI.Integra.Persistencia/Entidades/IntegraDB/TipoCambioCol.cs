using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TipoCambioCol : BaseIntegraEntity
    {
        public double PesosDolares { get; set; }
        public double DolaresPesos { get; set; }
        public DateTime Fecha { get; set; }
        public int IdMoneda { get; set; }
    }
}
