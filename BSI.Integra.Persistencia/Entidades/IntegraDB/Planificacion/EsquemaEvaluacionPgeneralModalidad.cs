using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class EsquemaEvaluacionPgeneralModalidad : BaseIntegraEntity
    {
        public int IdEsquemaEvaluacionPgeneral { get; set; }
        public int IdModalidadCurso { get; set; }
        public int? IdMigracion { get; set; }
    }
}
