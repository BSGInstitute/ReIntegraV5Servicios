
using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataInfMicroEvolucionDeudum : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        [StringLength(30)]
        public string? CodigoSector { get; set; }
        [StringLength(30)]
        public string? NombreSector { get; set; }
        [StringLength(30)]
        public string? TipoCuenta { get; set; }
        [StringLength(30)]
        public string? Trimestre { get; set; }
        [StringLength(30)]
        public string? Num { get; set; }
        public decimal? CupoInicial { get; set; }
        public decimal? Saldo { get; set; }
        public decimal? SaldoMora { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? PorcentajeDeuda { get; set; }
        [StringLength(20)]
        public string? CodigoMenorCalificacion { get; set; }
        [StringLength(20)]
        public string? TextoMenorCalificacion { get; set; }

        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
