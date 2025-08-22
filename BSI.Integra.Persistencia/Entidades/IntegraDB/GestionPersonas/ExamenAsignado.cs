using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class ExamenAsignado : BaseIntegraEntity
    {
        public int IdProcesoSeleccion { get; set; }
        public int IdExamen { get; set; }
        public int IdPostulante { get; set; }
        public bool? EstadoExamen { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdMigracion { get; set; }
        public bool? EstadoAcceso { get; set; }
        public int? VersionCentil { get; set; }
    }
}
