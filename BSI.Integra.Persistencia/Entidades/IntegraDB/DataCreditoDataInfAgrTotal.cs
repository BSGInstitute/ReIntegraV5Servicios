using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataInfAgrTotal : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        [StringLength(20)]
        public string? TipoMapeo { get; set; }
        [StringLength(40)]
        public string? CodigoTipo { get; set; }
        [StringLength(50)]
        public string? Tipo { get; set; }
        [StringLength(45)]
        public string? CalidadDeudor { get; set; }
        [StringLength(50)]
        public string? Participacion { get; set; }
        [StringLength(50)]
        public string? Cupo { get; set; }
        [StringLength(50)]
        public string? Saldo { get; set; }
        [StringLength(50)]
        public string? SaldoMora { get; set; }
        [StringLength(50)]
        public string? Cuota { get; set; }
        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
