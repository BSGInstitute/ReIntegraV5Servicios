using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ReporteFlujoCongeladoPorDium : BaseIntegraEntity
    {
        public string EstadoMatricula { get; set; } = null!;
        public int IdMatriculaCabecera { get; set; }
        public string CoordinadorAcademico { get; set; } = null!;
        public string CoordinadorCobranza { get; set; } = null!;
        public int IdPespecifico { get; set; }
        public string NombrePrograma { get; set; } = null!;
        public string CodigoMatricula { get; set; } = null!;
        public string NombreAlumno { get; set; } = null!;
        public DateTime FechaVencimiento { get; set; }
        public decimal MontoCuota { get; set; }
        public decimal TotalPagado { get; set; }
        public DateTime? FechaPago { get; set; }
        public DateTime? FechaProcesoPago { get; set; }
        public decimal SaldoPendiente { get; set; }
        public decimal SaldoPendienteDolar { get; set; }
        public decimal TotalCuotaDolar { get; set; }
        public decimal RealPagoDolar { get; set; }
        public string NroDocumento { get; set; } = null!;
        public string MonedaPago { get; set; } = null!;
        public decimal? TipoCambio { get; set; }
        public decimal Mora { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public int Version { get; set; }
        public bool Cancelado { get; set; }
        public string? Dni { get; set; }
        public string Email { get; set; } = null!;
        public DateTime FechaCongelamiento { get; set; }
        public double? MontoPagado { get; set; }
    }
}
