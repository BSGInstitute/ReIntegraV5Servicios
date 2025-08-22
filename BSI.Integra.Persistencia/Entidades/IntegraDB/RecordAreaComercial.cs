using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class RecordAreaComercial : BaseIntegraEntity
    {
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        public decimal Monto { get; set; }
        public int IdMonedaRecord { get; set; }
        public int IdTableroComercialUnidad { get; set; }
        public decimal Bono { get; set; }
        public int IdMonedaBono { get; set; }
        public bool VisualizarMonedaLocal { get; set; }
        public bool EsVigente { get; set; }
    }
}
