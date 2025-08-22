namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{

    public class ReportePendientePeriodoyCoordinadorSeparadoDTO
    {
        public string? PeriodoPorFechaVencimiento { get; set; }
        public string? Coordinador { get; set; }
        public decimal ProyectadoInicialCoordinador { get; set; }
        public decimal Actual { get; set; }
        public decimal Diferencia { get; set; }

        public decimal DiferenciaCambioFecha { get; set; }
        public decimal DiferenciaCambioMonto { get; set; }
        public decimal DiferenciaConsiderarMoraAdelantoSgteCuota { get; set; }
        public decimal DiferenciaModificacionNroCuotas { get; set; }

        public decimal DiferenciaRetirosCD { get; set; }
        public decimal DiferenciaRetirosSD { get; set; }
        public decimal MontoPagadoProy { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal IngresoVentas { get; set; }
        public decimal? MontoRecuperadoMes { get; set; }
        public decimal? PagosAdelantadoAcumulado { get; set; }
        public decimal PendientePorFactura { get; set; }
        public decimal PendienteSinFactura { get; set; }
        public decimal MontoVencido { get; set; }
        public decimal MontoPorVencer { get; set; }
        public decimal PagoPrevio { get; set; }
        public decimal PagoDentroMes { get; set; }
        public decimal PagoEnFechaVenc { get; set; }
        public decimal MatriculadosFechaPago { get; set; }
        public decimal MatriculadosFechaVencimiento { get; set; }
        public decimal ProyectadoInicialMes { get; set; }
        public decimal Modificacion { get; set; }

    }

    public class ReportePendienteMesCoordinadorFiltroDTO
    {
        public int PeriodoInicial { get; set; }
        public int PeriodoFin { get; set; }
        public DateTime FechaCorte1 { get; set; }
        public DateTime FechaCorte2 { get; set; }
        public DateTime FechaPagoInicial { get; set; }
        public DateTime FechaPagoFinal { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }

    public class ReportePendientePeriodoyCoordinadorSeparadoCierreDTO
    {
        public string? PeriodoPorFechaVencimiento { get; set; }
        public string? Coordinador { get; set; }
        public decimal ProyectadoInicialMes { get; set; }
        public decimal Actual { get; set; }
        public decimal Diferencia { get; set; }

        public decimal DiferenciaCambioFecha { get; set; }
        public decimal DiferenciaCambioMonto { get; set; }
        public decimal DiferenciaConsiderarMoraAdelantoSgteCuota { get; set; }
        public decimal DiferenciaModificacionNroCuotas { get; set; }

        public decimal DiferenciaRetirosCD { get; set; }
        public decimal DiferenciaRetirosSD { get; set; }
        public decimal MontoPagadoProy { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal IngresoVentas { get; set; }
        public decimal? MontoRecuperadoMes { get; set; }
        public decimal? PagosAdelantadoAcumulado { get; set; }
        public decimal PendientePorFactura { get; set; }
        public decimal PendienteSinFactura { get; set; }
        public decimal MontoVencido { get; set; }
        public decimal MontoPorVencer { get; set; }
        public decimal PagoPrevio { get; set; }
        public decimal PagoDentroMes { get; set; }
        public decimal PagoEnFechaVenc { get; set; }
        public decimal MatriculadosFechaPago { get; set; }
        public decimal MatriculadosFechaVencimiento { get; set; }
        public decimal ProyectadoInicialCoordinadora { get; set; }
        public decimal Modificacion { get; set; }

    }

    public class ResultadoGeneralMesCoordinadoraDTO{
        public List<ReportePendientePeriodoyCoordinadorSeparadoDTO> Periodo { get; set; }
        public List<ReportePendientePeriodoyCoordinadorSeparadoCierreDTO> Cierre { get; set; }
        public string FechaCierreActual { get; set; }
        public string FechaCierrePrevio { get; set; }
    }

    public class ReportePendientePeriodoCierreDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
        public string Coordinador { get; set; }
        public decimal Proyectado { get; set; }
        public decimal Actual { get; set; }
        public decimal Diferencia { get; set; }

        public decimal DiferenciaCambioFecha { get; set; }
        public decimal DiferenciaCambioMonto { get; set; }
        public decimal DiferenciaConsiderarMoraAdelantoSgteCuota { get; set; }
        public decimal DiferenciaModificacionNroCuotas { get; set; }

        public decimal DiferenciaRetirosCD { get; set; }
        public decimal DiferenciaRetirosSD { get; set; }
        public decimal MontoPagadoProy { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal IngresoVentas { get; set; }
        public decimal? MontoRecuperadoMes { get; set; }
        public decimal? PagosAdelantadoAcumulado { get; set; }
        public decimal ProyectadoVentas { get; set; }
        public decimal ModificacionesSinRetiro { get; set; }
        public decimal PendientePorFactura { get; set; }
        public decimal PendienteSinFactura { get; set; }
        public decimal MontoVencido { get; set; }
        public decimal MontoPorVencer { get; set; }
        public decimal PagoPrevio { get; set; }
        public decimal PagoDentroMes { get; set; }
        public decimal PagoEnFechaVenc { get; set; }
        public decimal ProyectadoInicial { get; set; }
        public decimal Modificacion { get; set; }
        public decimal? ProyectadoCierre { get; set; }
        public decimal? ActualCierre { get; set; }
        public decimal? MontoPagadoCierre { get; set; }
        public decimal? IngresoVentasCierre { get; set; }
        public decimal? MontoRecuperadoMesCierre { get; set; }
        public decimal? PagosAdelantadoAcumuladoCierre { get; set; }
        public decimal? MontoVencidoCierre { get; set; }
        public decimal? PagoPrevioCierre { get; set; }
        public decimal? PagoDentroMesCierre { get; set; }
        public decimal RetiradosInicial { get; set; }
        
    }


}
