using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoConsultum : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        public DateTime? Fecha { get; set; }
        [StringLength(20)]
        public string? TipoCuenta { get; set; }
        [StringLength(50)]
        public string? Entidad { get; set; }
        [StringLength(50)]
        public string? Oficina { get; set; }
        [StringLength(50)]
        public string? Ciudad { get; set; }
        [StringLength(20)]
        public string? Razon { get; set; }
        [StringLength(20)]
        public string? Cantidad { get; set; }
        [StringLength(25)]
        public string? NitSuscriptor { get; set; }
        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
