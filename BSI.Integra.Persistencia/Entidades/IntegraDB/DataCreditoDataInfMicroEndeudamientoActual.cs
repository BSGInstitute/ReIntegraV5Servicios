using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataInfMicroEndeudamientoActual : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        [StringLength(40)]
        public string? SectorCodigoSector { get; set; }
        [StringLength(40)]
        public string? TipoCuenta { get; set; }
        [StringLength(40)]
        public string? TipoUsuario { get; set; }
        [StringLength(40)]
        public string? EstadoActual { get; set; }
        [StringLength(40)]
        public string? Calificacion { get; set; }
        public decimal? ValorInicial { get; set; }
        public decimal? SaldoActual { get; set; }
        public decimal? SaldoMora { get; set; }
        public decimal? CuotaMes { get; set; }
        public bool? ComportamientoNegativo { get; set; }
        public decimal? TotalDeudaCarteras { get; set; }

        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
