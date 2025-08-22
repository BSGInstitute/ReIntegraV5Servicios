using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TipoCategoriaOrigen : BaseIntegraEntity
    {
        [Required]
        [StringLength(150)]
        public string Nombre { get; set; } = null!;
        [StringLength(300)]
        [Required]
        public string? Descripcion { get; set; }
        [Required]
        public int? Meta { get; set; }
        [Required]
        public int? Orden { get; set; }
        [Required]
        public int? OportunidadMaxima { get; set; }

    }
}
