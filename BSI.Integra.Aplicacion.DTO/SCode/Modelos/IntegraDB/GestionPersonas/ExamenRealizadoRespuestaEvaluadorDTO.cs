
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class ExamenRealizadoRespuestaEvaluadorDTO
    {
        public int Id { get; set; }
        public int IdExamenAsignadoEvaluador { get; set; }
        public int IdPregunta { get; set; }
        public int IdRespuestaPregunta { get; set; }
        public string? TextoRespuesta { get; set; }
        public int? IdMigracion { get; set; }
    }
}
