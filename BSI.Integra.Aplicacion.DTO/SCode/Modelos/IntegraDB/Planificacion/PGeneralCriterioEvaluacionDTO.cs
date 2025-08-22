
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PGeneralCriterioEvaluacionDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public int IdModalidadCurso { get; set; }
        public string? Nombre { get; set; } = null!;
        public int Porcentaje { get; set; }
        public int? IdCriterioEvaluacion { get; set; }
        public int? IdTipoPromedio { get; set; }
    }
}
