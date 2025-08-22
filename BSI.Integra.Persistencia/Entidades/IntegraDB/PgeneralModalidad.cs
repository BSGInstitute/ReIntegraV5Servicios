using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PgeneralModalidad : BaseIntegraEntity
    {

        public int Id { get; set; }

        public int IdPgeneral { get; set; }

        public int IdModalidadCurso { get; set; }

        public virtual TModalidadCurso IdModalidadCursoNavigation { get; set; } = null!;
        public virtual TPgeneral IdPgeneralNavigation { get; set; } = null!;

    }
}
