using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PostulanteFormacion : BaseIntegraEntity
    {
        public int IdPostulante { get; set; }
        public int IdCentroEstudio { get; set; }
        public int IdTipoEstudio { get; set; }
        public int? IdAreaFormacion { get; set; }
        public int? IdEstadoEstudio { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdMigracion { get; set; }
        public string? OtraInstitucion { get; set; }
        public string? OtraCarrera { get; set; }
        public bool? AlaActualidad { get; set; }
        public string? TurnoEstudio { get; set; }
        public int? IdPais { get; set; }
    }
}
