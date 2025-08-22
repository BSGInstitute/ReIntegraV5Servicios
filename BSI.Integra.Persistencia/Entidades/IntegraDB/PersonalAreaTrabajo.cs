using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PersonalAreaTrabajo : BaseIntegraEntity
    {
        [StringLength(3)]
        public string Codigo { get; set; } = null!;
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [StringLength(500)]
        public string Descripcion { get; set; } = null!;
    }
}
