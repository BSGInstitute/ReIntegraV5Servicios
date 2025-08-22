using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataInfMicroPerfilGeneral : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        [StringLength(30)]
        public string Tipo { get; set; } = null!;
        [StringLength(30)]
        public string? SectorFinanciero { get; set; }
        [StringLength(30)]
        public string? SectorCooperativo { get; set; }
        [StringLength(30)]
        public string? SectorReal { get; set; }
        [StringLength(30)]
        public string? SectorTelcos { get; set; }
        [StringLength(30)]
        public string? TotalComoPrincipal { get; set; }
        [StringLength(30)]
        public string? TotalComoCodeudorYotros { get; set; }
        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
