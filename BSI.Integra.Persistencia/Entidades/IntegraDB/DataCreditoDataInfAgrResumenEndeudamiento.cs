using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataInfAgrResumenEndeudamiento : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        public DateTime? TrimestreFecha { get; set; }
        [StringLength(25)]
        public string? SectorSector { get; set; }
        [StringLength(20)]
        public string? SectorCodigoSector { get; set; }
        [StringLength(60)]
        public string? SectorGarantiaAdmisible { get; set; }
        [StringLength(60)]
        public string? SectorGarantiaOtro { get; set; }
        [StringLength(25)]
        public string? CarteraTipo { get; set; }
        public int? CarteraNumeroCuentas { get; set; }
        public decimal? CarteraValor { get; set; }

        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
