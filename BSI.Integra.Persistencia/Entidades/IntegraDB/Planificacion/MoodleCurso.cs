using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class MoodleCurso : BaseIntegraEntity
    {
        public int? IdCursoMoodle { get; set; }
        public int? IdCategoriaMoodle { get; set; }
        public string? Nombre { get; set; } = null!;




    }
}
