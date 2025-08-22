using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ConfiguracionFija : BaseIntegraEntity
    {
        [StringLength(50)]
        public string Codigo { get; set; } = null!;
        [StringLength(50)]
        public string NombreTabla { get; set; } = null!;
        public int IdTabla { get; set; }
        [StringLength(50)]
        public string NombreColumna { get; set; } = null!;
        [StringLength(50)]
        public string TipoDato { get; set; } = null!;
        [StringLength(50)]
        public string Valor { get; set; } = null!;
    }
}
