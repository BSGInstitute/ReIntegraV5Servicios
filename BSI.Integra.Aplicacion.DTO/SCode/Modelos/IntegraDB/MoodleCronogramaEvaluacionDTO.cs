namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MoodleCronogramaEvaluacionDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdCursoMoodle { get; set; }
        public int? IdEvaluacionMoodle { get; set; }
        public string NombreEvaluacion { get; set; }
        public DateTime FechaEstimada { get; set; }
        public int Orden { get; set; }
        public int Version { get; set; }
        public int? IdMigracion { get; set; }
    }
    public class VersionCronogramaAutoEvaluacionDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int Version { get; set; }
    }
    public class IdentificadorCursoMoodlePorMatriculaComboDTO
    {
        public int IdCursoMoodle { get; set; }
        public int IdUsuarioMoodle { get; set; }
        public string NombreCurso { get; set; }
        public int IdOportunidad { get; set; }
        public int IdMatriculaCabecera { get; set; }
    }
    public class CronogramaListaCursosOnlineV2PromedioDTO
    {
        public string CodigoMatricula { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdCursoMoodle { get; set; }
        public string NombreCurso { get; set; }
        public DateTime? FechaCronograma { get; set; }
        public DateTime? FechaRendicion { get; set; }
        public decimal? Promedio { get; set; }
    }
}
