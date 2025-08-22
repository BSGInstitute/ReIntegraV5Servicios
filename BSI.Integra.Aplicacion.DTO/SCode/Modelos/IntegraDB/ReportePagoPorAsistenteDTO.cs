namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class filtroReportePagoPorAsistenteDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string? IdsConfiguracion { get; set; }

    }

    public class ReportePagoPorAsistenteDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string EstadoMatricula { get; set; }
        public string Alumno { get; set; }
        public string AgrupacionMat { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Cuota { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public decimal Mora { get; set; }
        public string NroCuota { get; set; }
        public string Moneda { get; set; }
        public decimal TotalCuotaD { get; set; }
        public decimal RealPagoD { get; set; }
        public decimal SaldoPendienteD { get; set; }
        public string CodigoMatricula { get; set; }
        public string Coordinadoracobranza { get; set; }
        public string PaisAlumno { get; set; }
    }
}
