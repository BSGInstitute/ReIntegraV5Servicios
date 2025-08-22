using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataInfMicroImagenTendenciaEndeudamiento : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        public string? Serie { get; set; }
        public decimal? Valor { get; set; }
        public DateTime? Fecha { get; set; }
        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
