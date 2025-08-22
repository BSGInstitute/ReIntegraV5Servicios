using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class IvrEjecucion : BaseIntegraEntity
    {
        [StringLength(50)]
        [Required]
        public string Nombre { get; set; } = null!;
        [StringLength(200)]
        [Required]
        public string Descripcion { get; set; } = null!;
    }
}
