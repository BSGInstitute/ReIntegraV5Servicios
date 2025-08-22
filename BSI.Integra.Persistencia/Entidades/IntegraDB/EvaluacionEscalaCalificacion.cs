using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class EvaluacionEscalaCalificacion : BaseIntegraEntity
    {
        public int IdModalidadCurso { get; set; }
        [StringLength(15)]
        public string CodigoCiudad { get; set; } = null!;
        public decimal EscalaCalificacion { get; set; }
        public decimal NotaAprobatoria { get; set; }
        public int RedondeoDecimales { get; set; }
        [StringLength(50)]
        public string EscalaTexto { get; set; } = null!;
        [StringLength(50)]
        public string NotaAprobatoriaTexto { get; set; } = null!;
    }
}
