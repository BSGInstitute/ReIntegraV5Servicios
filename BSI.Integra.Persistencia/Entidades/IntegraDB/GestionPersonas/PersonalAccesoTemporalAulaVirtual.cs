using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PersonalAccesoTemporalAulaVirtual : BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public int IdPespecificoPadre { get; set; }
        public int IdPespecificoHijo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int? IdMigracion { get; set; }
        public bool? EvaluacionHabilitada { get; set; }
    }
}
