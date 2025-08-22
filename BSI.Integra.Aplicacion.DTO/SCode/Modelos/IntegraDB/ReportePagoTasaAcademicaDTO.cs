namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ComboConceptoDTO
    {
        public string Nombre { get; set; }   
        
    }

    public class filtroReporteTasaAcademicaDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdMatricula { get; set; }
        public string? Concepto { get; set; }

    }

    public class ReporteTasasAcademicasDTO
    {
        public string CodPrograma { get; set; }
        public string CodMat { get; set; }
        public string Alumno { get; set; }
        public string Concepto { get; set; }    
        public decimal Monto { get; set; }
        public string Moneda { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal MontoDolares { get; set; }
    }
}
