using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class IvrPlantilla : BaseIntegraEntity
    {
        [StringLength(100)]
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Texto { get; set; } = null!;
        public bool MenuOpcion { get; set; }
        public string? TextoMenu { get; set; }
        public bool Activo { get; set; }
    }
}
