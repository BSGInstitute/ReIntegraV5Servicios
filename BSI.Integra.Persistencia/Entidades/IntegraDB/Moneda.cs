using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Moneda : BaseIntegraEntity
    {
        [StringLength(30)]
        public string Nombre { get; set; } = null!;
        [StringLength(15)]
        public string NombreCorto { get; set; } = null!;
        [StringLength(20)]
        public string NombrePlural { get; set; } = null!;
        [StringLength(10)]
        public string Simbolo { get; set; } = null!;
        [StringLength(10)]
        public string? Codigo { get; set; }
        public int IdPais { get; set; }
        public int DigitoFinanzas { get; set; }
        public bool? ValidaProcesoSeleccion { get; set; }
        public bool? VisualizarTableroComercial { get; set; }
        public bool? VisualizarFinanzas { get; set; }
        public decimal? PorcentajeMora { get; set; }
    }
}
