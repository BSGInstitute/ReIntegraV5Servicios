using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class ExamenRealizadoRespuestaEvaluador : BaseIntegraEntity
    {
        public int IdExamenAsignadoEvaluador { get; set; }
        public int IdPregunta { get; set; }
        public int IdRespuestaPregunta { get; set; }
        public string? TextoRespuesta { get; set; }
        public int? IdMigracion { get; set; }
    }
}
