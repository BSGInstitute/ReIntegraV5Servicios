using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class MaterialAccion : BaseIntegraEntity
    {
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        [StringLength(500)]
        public string Descripcion { get; set; } = null!;
    }
}
