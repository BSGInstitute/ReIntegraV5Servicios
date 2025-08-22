namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class EsquemaEvaluacionDetalleDTO
    {
        public int Id { get; set; }
        public int IdEsquemaEvaluacion { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public int Ponderacion { get; set; }
    }
    public class EsquemaEvaluacionDetalleCompuestoDTO
    {
        public int Id { get; set; }
        public int IdEsquemaEvaluacion { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public string NombreCriterioEvaluacion { get; set; }
        public int Ponderacion { get; set; }
    }
}