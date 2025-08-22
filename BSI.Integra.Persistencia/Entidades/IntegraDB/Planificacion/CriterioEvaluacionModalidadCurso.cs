using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class CriterioEvaluacionModalidadCurso : BaseIntegraEntity
    {
        public int IdCriterioEvaluacion { get; set; }
        public int IdModalidadCurso { get; set; }
        public int? IdMigracion { get; set; }
        
    }
}
