namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CronogramaAutoEvaluacionDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdCurso { get; set; }
        public DateTime? FechaRendicion { get; set; }
        public int Nota { get; set; }
        public string NombreAutoEvaluacion { get; set; }
        public string NombreCurso { get; set; }
        public DateTime? FechaCronograma { get; set; }
    }
    public class CronogramaAutoEvaluacionV2DTO
    {
        public int Id { get; set; }
        public int? IdAlumno { get; set; }
        public string IdMatriculaCabecera { get; set; }
        public int IdCursoMoodle { get; set; }
        public int IdEvaluacionMoodle { get; set; }
        public string NombreCurso { get; set; }
        public string NombreEvaluacion { get; set; }
        public DateTime? FechaCronograma { get; set; }
        public DateTime? FechaRendicion { get; set; }
        public decimal? Nota { get; set; }
        public int Orden { get; set; }
        public int Version { get; set; }
    }
    public class RespuestaWebDTO
    {
        public string Mensaje { get; set; }
        public bool Estado { get; set; }
    }
    public class AutoEvaluacionCronogramaDetalleDTO
    {
        public string NombreAutoEvaluacion { get; set; }
        public DateTime FechaCronograma { get; set; }
    }
    public class AutoEvaluacionCompletaCronogramaDetalleDTO
    {
        public string NombreAutoEvaluacion { get; set; }
        public int Nota { get; set; }
        public DateTime FechaCronograma { get; set; }
    }
}
