using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataInfAgrComposicionPortafolio : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        [StringLength(50)]
        public string? Tipo { get; set; }
        [StringLength(50)]
        public string? CalidadDeudor { get; set; }
        public decimal? Porcentaje { get; set; }
        public int? Cantidad { get; set; }
        [StringLength(50)]
        public string? EstadoCodigo { get; set; }
        public int? EstadoCantidad { get; set; }

        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
