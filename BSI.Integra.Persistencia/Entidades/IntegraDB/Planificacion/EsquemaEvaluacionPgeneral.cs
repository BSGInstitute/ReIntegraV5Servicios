using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class EsquemaEvaluacionPgeneral : BaseIntegraEntity
    {
        public int IdEsquemaEvaluacion { get; set; }
        public int IdPgeneral { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? EsquemaPredeterminado { get; set; }
        public List<EsquemaEvaluacionPgeneralDetalle> EsquemaEvaluacionPgeneralDetalles { get; set; }
        public List<EsquemaEvaluacionPgeneralModalidad> EsquemaEvaluacionPgeneralModalidads { get; set; }
        public List<EsquemaEvaluacionPgeneralProveedor> EsquemaEvaluacionPgeneralProveedors { get; set; }
    }
}
