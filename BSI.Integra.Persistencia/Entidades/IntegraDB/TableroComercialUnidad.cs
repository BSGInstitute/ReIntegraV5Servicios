using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TableroComercialUnidad : BaseIntegraEntity
    {
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        public int Valor { get; set; }
        [StringLength(10)]
        public string? Simbolo { get; set; }
    }
}
