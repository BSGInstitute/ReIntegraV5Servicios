using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TableroComercialCategoriaAsesor : BaseIntegraEntity
    {
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        public decimal MontoVenta { get; set; }
        public int IdMonedaVenta { get; set; }
        public int IdTableroComercialUnidadVenta { get; set; }
        public decimal MontoPremio { get; set; }
        public int IdMonedaPremio { get; set; }
        public bool VisualizarMonedaLocal { get; set; }
    }
}
