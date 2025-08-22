using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class MoodleCronogramaEvaluacion : BaseIntegraEntity
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdCursoMoodle { get; set; }
        public int? IdEvaluacionMoodle { get; set; }
        [StringLength(50)]
        public string NombreEvaluacion { get; set; } = null!;
        public DateTime FechaEstimada { get; set; }
        public int Orden { get; set; }
        public int Version { get; set; }
    }
}
