using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class HistoricoProductoProveedor : BaseIntegraEntity
    {
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
        public decimal CostoMonedaOrigen { get; set; }
        public decimal CostoDolares { get; set; }
        public int IdMoneda { get; set; }
        public decimal Precio { get; set; }
        public decimal TipoCambio { get; set; }
        public int? IdCondicionPago { get; set; }
        public int IdCondicionTipoPago { get; set; }
        public int Version { get; set; }
        [StringLength(500)]
        public string Observaciones { get; set; } = null!;

    }
}
