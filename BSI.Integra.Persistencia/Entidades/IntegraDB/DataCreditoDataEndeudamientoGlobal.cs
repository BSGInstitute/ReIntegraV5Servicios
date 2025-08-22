using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataEndeudamientoGlobal : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        [StringLength(20)]
        public string? Calificacion { get; set; }
        [StringLength(20)]
        public string? Fuente { get; set; }
        [StringLength(20)]
        public string? SaldoPendiente { get; set; }
        [StringLength(20)]
        public string? TipoCredito { get; set; }
        [StringLength(20)]
        public string? Moneda { get; set; }
        [StringLength(20)]
        public string? NumeroCreditos { get; set; }
        [StringLength(20)]
        public string? Independiente { get; set; }
        public DateTime? FechaReporte { get; set; }
        [StringLength(50)]
        public string? EntidadNombre { get; set; }
        [StringLength(20)]
        public string? EntidadNit { get; set; }
        [StringLength(20)]
        public string? EntidadSector { get; set; }
        [StringLength(20)]
        public string? GarantiaTipo { get; set; }
        [StringLength(20)]
        public string? GarantiaValor { get; set; }
        [StringLength(60)]
        public string? Llave { get; set; }

        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
