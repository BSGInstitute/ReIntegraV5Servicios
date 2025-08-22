using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public partial class PgeneralCriterioEvaluacionHijo : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public bool ConsiderarNota { get; set; }
        public int? Porcentaje { get; set; }
        public int IdModalidadCurso { get; set; }
        public int IdTipoPromedio { get; set; }
        public int IdPgeneralHijo { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdCriterioEvaluacion { get; set; }
    }
}
