using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class ExamenAsignadoEvaluador : BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public int IdPostulante { get; set; }
        public int IdExamen { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public bool EstadoExamen { get; set; }
        public int? IdMigracion { get; set; }
        public virtual ICollection<ExamenRealizadoRespuestaEvaluador> ExamenRealizadoRespuestaEvaluadors { get; set; }
    }
}
