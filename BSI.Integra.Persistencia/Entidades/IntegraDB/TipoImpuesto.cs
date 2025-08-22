using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TipoImpuesto : BaseIntegraEntity
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; } = null!;
        [Required]
        public int Valor { get; set; }
        [Required]
        public int IdPais { get; set; }
        [Required]
        public bool Activo { get; set; }

    }
}
