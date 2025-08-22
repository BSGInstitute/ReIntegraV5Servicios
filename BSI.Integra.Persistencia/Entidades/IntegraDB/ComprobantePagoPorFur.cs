using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ComprobantePagoPorFur : BaseIntegraEntity
    {
        public int IdComprobantePago { get; set; }
        public int IdFur { get; set; }
        public decimal Monto { get; set; }
    }
}
