using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PgeneralVersionPrograma : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }

        public int? IdVersionPrograma { get; set; }

        public int? Duracion { get; set; }

        public virtual TPgeneral IdPgeneralNavigation { get; set; } = null!;

    }
}
