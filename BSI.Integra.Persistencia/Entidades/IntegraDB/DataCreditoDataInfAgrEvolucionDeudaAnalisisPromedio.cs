using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? CupoTotal { get; set; }
        public decimal? Saldo { get; set; }
        [StringLength(30)]
        public string? Porcentaje { get; set; }
        public decimal? Score { get; set; }
        public int? Calificacion { get; set; }
        public decimal? AperturaCuentas { get; set; }
        public decimal? CierreCuentas { get; set; }
        [StringLength(25)]
        public string? TotalAbiertas { get; set; }
        [StringLength(25)]
        public string? TotalCerradas { get; set; }
        public decimal? MoraMaxima { get; set; }

        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
