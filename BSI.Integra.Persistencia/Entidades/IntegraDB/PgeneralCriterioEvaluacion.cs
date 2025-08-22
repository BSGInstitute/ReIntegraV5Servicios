using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public partial class PgeneralCriterioEvaluacion : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public int IdModalidadCurso { get; set; }
        public string Nombre { get; set; } = null!;
        public int Porcentaje { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdCriterioEvaluacion { get; set; }
        public int? IdTipoPromedio { get; set; }
    }
}
