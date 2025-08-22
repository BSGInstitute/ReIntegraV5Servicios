using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PostulanteAccesoTemporalAulaVirtual : BaseIntegraEntity
    {
        public int IdPostulante { get; set; }
        public int IdPespecificoPadre { get; set; }
        public int IdPespecificoHijo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdPostulanteProcesoSeleccion { get; set; }
        public int? IdExamen { get; set; }
    }
}
