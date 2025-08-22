using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PgeneralAsubPgeneral : BaseIntegraEntity
    {
        public int IdPgeneralPadre { get; set; }
        public int IdPgeneralHijo { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? Orden { get; set; }
        public bool? EsVisiblePortal { get; set; }
        public int? IdCiclo { get; set; }
        public int? IdModulo { get; set; }
        public List<PgeneralAsubPgeneralVersionPrograma> PgeneralAsubPgeneralVersionProgramas { get; set; }
    }
}
