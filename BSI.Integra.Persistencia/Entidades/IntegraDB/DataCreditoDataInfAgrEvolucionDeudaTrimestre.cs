using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataInfAgrEvolucionDeudaTrimestre : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? Cupototal { get; set; }
        public decimal? Saldo { get; set; }
        [StringLength(25)]
        public string? PorcentajeUso { get; set; }
        public decimal? Score { get; set; }
        [StringLength(25)]
        public string? Calificacion { get; set; }
        [StringLength(25)]
        public string? AperturaCuentas { get; set; }
        [StringLength(25)]
        public string? CierreCuentas { get; set; }
        public int? TotalAbiertas { get; set; }
        public int? TotalCerradas { get; set; }
        [StringLength(25)]
        public string? MoraMaxima { get; set; }
        public int? MesesMoraMaxima { get; set; }
        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
