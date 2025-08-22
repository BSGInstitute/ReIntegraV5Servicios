using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class AlumnoCuponRegistro : BaseIntegraEntity
    {
        public int IdAlumno { get; set; }
        [StringLength(7)]
        public string CodigoCupon { get; set; } = null!;
        public int? IdPersonal { get; set; }
        [StringLength(100)]
        public string? AreaVentas { get; set; }
    }
}
