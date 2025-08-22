namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{


    public class filtroReportePorEstadoMatriculaDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string? IdsConfiguracionPeriodo { get; set; }
        public string? IdsCoordinadora { get; set; }
        public string? IdsEstados { get; set; }

    }

    public class ReporteMatriculadoDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public DateTime FechaMatricula { get; set; }
        public string AgrupacionMat { get; set; }
        public string EstadoMatricula { get; set; }
        public string PaisAlumno { get; set; }
        public string CoordinadoraCobranza { get; set; }
    }

    public class ReportePorEstadosMatriculaDTO
    {
        public string CoordinadoraCobranza { get; set; }
        public string EstadoMatricula { get; set; }
        public string AgrupacionMat { get; set; }
        public string Periodo { get; set; }
        public decimal TotalCuotaD { get; set; }
        public decimal RealPagoD { get; set; }
        public decimal SaldoPendienteD { get; set; }

    }

    public class ReportePorEstadosMatriculaPrincipalDTO
    {
        public string CoordinadoraCobranza { get; set; }
        public string EstadoMatricula { get; set; }
        public string AgrupacionMat { get; set; }
    }

    public class ReportePorEstadosMatriculaDetalleDTO
    {
        public string Periodo { get; set; }
        public List<PeriodoFechaVencimientoDTO> Detalle { get; set; }
    }


    public class PeriodoFechaVencimientoDTO
    {
        public string CoordinadoraCobranza { get; set; }
        public decimal Proyectado { get; set; }
        public decimal Real { get; set; }
        public decimal Pendiente { get; set; }
        public string EstadoMatricula { get; set; }
        public string AgrupacionMat { get; set; }

    }

    public class ReportePorEstadosMatriculaFinalDTO
    {
        public List<ReportePorEstadosMatriculaPrincipalDTO> ListaPrincipal { get; set; }
        public List<ReportePorEstadosMatriculaDetalleDTO> Detalle { get; set; }
    }
}
