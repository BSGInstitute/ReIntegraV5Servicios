using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class EsquemaEvaluacionDetalle : BaseIntegraEntity
    {
        public int IdEsquemaEvaluacion { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public int Ponderacion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
