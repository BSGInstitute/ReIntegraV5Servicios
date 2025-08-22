namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PagoAlumnoIngresosDTO
    {
        public string? CodigoMatricula { get; set; }
        public int? IdCronogramaPagoDetalleFinal { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int? NroCuota { get; set; }
        public int? NroSubCuota { get; set; }
        public decimal? CuotaDolares { get; set; }
        public decimal? MontoPagado { get; set; }
        public decimal? MontoPagadoTipoCambioFechaPago { get; set; }
        public string? PeriodoPorFechaVencimiento { get; set; }
        public string? PeriodoFechaPago { get; set; }
        public DateTime? FechaPago { get; set; }
        public string? DiaPago { get; set; }
        public DateTime? FechaPagoReal { get; set; }
        public int? DiasDeposito { get; set; }
        public int? DiasDisponible { get; set; }
        public bool? CuentaFeriados { get; set; }
        public bool? ConsideraVSD { get; set; }
        public bool? ConsiderarDiasHabilesLV { get; set; }
        public bool? ConsiderarDiasHabilesLS { get; set; }
        public DateTime? FechaIngresoEnCuenta { get; set; }
        public DateTime? FechaCuota { get; set; }
        public string? EstadoEfectivo { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public DateTime? FechaPagoOriginal { get; set; }
        public DateTime? FechaMatricula { get; set; }
        public decimal? PorcentajeComision { get; set; }
        public decimal? CobroComisionMontoPagado { get; set; }
        public int? IdTipoMovimientoCaja { get; set; }
    }

    public class PagosIngresosDTO
    {
        public int? IdAlumno { get; set; }
        public string? Dni { get; set; }
        public string? Email { get; set; }
        public string? Modalidad { get; set; }
        public int? Correlativo { get; set; }
        public int? IdCiudad { get; set; }
        public string? Ciudad { get; set; }
        public string? NombrePrograma { get; set; }
        public string? Programa { get; set; }
        public string? CodigoAlumno { get; set; }
        public string? Alumno { get; set; }
        public string? Comprobante { get; set; }
        public int? IdComprobante { get; set; }
        public string? Numero { get; set; }
        public string? MonedaPago { get; set; }
        public decimal? TipoCambio { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? Mora { get; set; }
        public decimal? TotalPagado { get; set; }
        public decimal? TotalPagadoDisponible { get; set; }
        public DateTime? FechaPagoOriginal { get; set; }
        public DateTime? FechaPago { get; set; }
        public string? DiaPago { get; set; }
        public DateTime? FechaPagoReal { get; set; }
        public int? DiasDeposito { get; set; }
        public int? DiasDisponible { get; set; }
        public bool? CuentaFeriados { get; set; }
        public bool? ConsideraVSD { get; set; }
        public bool? ConsideraDiasHabilesLV { get; set; }
        public bool? ConsideraDiasHabilesLS { get; set; }
        public decimal? PorcentajeCobro { get; set; }
        public DateTime? FechaDepositaron { get; set; }
        public DateTime? FechaDisponible { get; set; }
        public DateTime?  FechaProcesoPago { get; set; }
        public string? EstadoEfectivo { get; set; }
        public string? Cuota_SubCuota { get; set; }
        public string? FechaCuota { get; set; }
        public string? Observaciones { get; set; }
        public string? FormaIngreso { get; set; }
        public string? EstadoCuota { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int? IdModalidad { get; set; }
        public int? IdCentroCosto { get; set; }
    }

    public class ReporteCompletoDTO
    {
        public List<PagoAlumnoIngresosDTO> resultVentas { get; set; }
        public List<PagoAlumnoIngresosDTO> resultOperaciones { get; set; }
        public List<PagosIngresosDTO> resultOperacionesTipoCambio { get; set; }
        public List<PagoAlumnoIngresosDTO> resultOtrosIngresosEgresos { get; set; }
        public List<PagosIngresosDTO> resultReportePagos { get; set; }
        public List<PagosIngresosDTO> resultReportePagosPosterior { get; set; }
        public List<PagosIngresosDTO> resultReportePagosAnterior { get; set; }
        public List<PagosIngresosDTO> resultReporteGestionCobranza { get; set; }
        public List<PagosIngresosDTO> resultReporteTasasAcademicas { get; set; }
        public List<PagosIngresosDTO> resultReportePagosAnteriorDeposito { get; set; }
        public List<PagosIngresosDTO> resultReportePagosPosteriorDeposito { get; set; }
        public FiltroFechaDTO Filtro { get; set; }
    }

    public class ReporteIngresosDetalleDTO
    {
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
    }
}
