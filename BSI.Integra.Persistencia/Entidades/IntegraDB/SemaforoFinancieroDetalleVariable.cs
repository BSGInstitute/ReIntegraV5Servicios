using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SemaforoFinancieroDetalleVariable : BaseIntegraEntity
    {
        public int IdSemaforoFinancieroDetalle { get; set; }
        public int IdSemaforoFinancieroVariable { get; set; }
        public decimal? ValorMinimo { get; set; }
        public int? IdMoneda { get; set; }
        public decimal? ValorMaximo { get; set; }
    }
}
