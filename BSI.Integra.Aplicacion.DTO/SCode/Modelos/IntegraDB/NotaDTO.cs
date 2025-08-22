namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class NotaDTO
    {
        public int Id { get; set; }
        public int Nota { get; set; }
    }
    public class NotaPresencialDTO
    {
        public int? IdNota { get; set; }
        public int? IdEvaluacion { get; set; }
        public int IdPEspecifico { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string NombrePEspecifico { get; set; }
        public string NombreEvaluacion { get; set; }
        public decimal? Porcentaje { get; set; }
        public decimal? Nota { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string DuracionCurso { get; set; }
        public int? IdAlumno { get; set; }
    }
    public class NotaPresencialPromedioDTO
    {
        public int IdPEspecifico { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdAlumno { get; set; }
        public string NombrePEspecifico { get; set; }
        public string FechaInicio { get; set; }
        public string FechaTermino { get; set; }
        public string Promedio { get; set; }
    }
    public class NotaPresencialPromedioEspecificoDTO
    {
        public int IdPEspecifico { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdAlumno { get; set; }
        public string NombrePEspecifico { get; set; }
        public string FechaInicio { get; set; }
        public string FechaTermino { get; set; }
        public string Promedio { get; set; }
        public string Porcentaje { get; set; }
        public string Nota { get; set; }
    }
}
