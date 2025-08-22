using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PlantillaClaveValor : BaseIntegraEntity
    {
        [StringLength(300)]
        public string Clave { get; set; } = null!;
        public string? Valor { get; set; }
        public string? Etiquetas { get; set; }
        public int IdPlantilla { get; set; }

    }
}
