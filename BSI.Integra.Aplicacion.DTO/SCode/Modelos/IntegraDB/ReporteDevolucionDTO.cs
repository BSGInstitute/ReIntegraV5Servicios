namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteDevolucionDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
        public string TipoRetiro { get; set; }
        public int IdPrograma { get; set; }
        public string Programa { get; set; }
        public int IdAlumno { get; set; }
        public string Alumno { get; set; }
        public DateTime? Fecha { get; set; } = null;
        public decimal? MontoDevolucion { get; set; } = null;
        public int IdMatricula { get; set; }
        public string CodigoMatricula { get; set; }
        public string Observaciones { get; set; }

    }

    public class CongelarDatoDTO
    {
        public DateTime FechaCongelamiento { get; set; }
    }
}
