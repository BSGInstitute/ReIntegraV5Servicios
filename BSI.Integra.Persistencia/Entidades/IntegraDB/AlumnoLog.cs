using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class AlumnoLog : BaseIntegraEntity
    {
        public int IdAlumno { get; set; }
        [StringLength(100)]
        public string CampoActualizado { get; set; } = null!;
        [StringLength(1000)]
        public string ValorAnterior { get; set; } = null!;
        [StringLength(1000)]
        public string ValorNuevo { get; set; } = null!;
    }
}
