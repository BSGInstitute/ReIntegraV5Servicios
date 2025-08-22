namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteResumenMontosFiltroDTO
    {
        public int PeriodoActual { get; set; }
        public int PeriodoCierre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string IdPais { get; set; }
        public bool IsOtrosPais { get; set; }
        public bool IsTodo { get; set; }
    }
  

    public class ReporteResumenMontosCierreDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
        public decimal Actual { get; set; }
        public decimal MontoPagado { get; set; }
    }

    public class ReporteResumenMontosCambiosDTO
    {
        public int IdMatricula { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public decimal MontoProyectado { get; set; }
        public decimal MontoActual { get; set; }
        public decimal TipoCambio { get; set; }
        public string PeriodoProyectado { get; set; }
        public string PeriodoActual { get; set; }
        public decimal Diferencia { get; set; }
        public string Cambio { get; set; }
        public int IdTipoModalidad { get; set; }
    }

    public class ReporteResumenMontosDiferenciasDTO
    {
        public int IdMatricula { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public decimal MontoProyectado { get; set; }
        public decimal MontoActual { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal PruebaCuota { get; set; }
        public int Version { get; set; }
        public string PeriodoaProyectado { get; set; }
        public string PruebaFechaVencimiento { get; set; }
        public string PeriodoActual { get; set; }
        public decimal Diferencia { get; set; }
        public string DescripcionCambio { get; set; }
        public string DetalleCambio { get; set; }
    }


    public class ReporteResumenMontosDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
        public int IdTipoModalidad { get; set; }
        public decimal Proyectado { get; set; }
        public decimal Actual { get; set; }
        public decimal DiferenciaCambioFecha { get; set; }
        public decimal DiferenciaCambioMonto { get; set; }
        public decimal DiferenciaConsiderarMoraAdelantoSgteCuota { get; set; }
        public decimal DiferenciaModificacionNroCuotas { get; set; }
        public decimal Retiro_CD { get; set; }
        public decimal Retiro_SD { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal DiferenciaPorModificacion { get; set; }
        public int NuevaConsultoria { get; set; }
        public decimal NuevasMatriculas { get; set; }
        public decimal IngresoRealNuevasMatriculas { get; set; }
        public decimal IngresoRealNuevasMatriculasFechaPago { get; set; }
        public decimal PendientMesOrdenServicio { get; set; }
        public decimal PendientMesSinOrdenServicio { get; set; }
        public decimal RetirosCD_Mes { get; set; }
        public decimal RetirosSD_Mes { get; set; }
        public decimal IncrementosDisminucionesCronograma { get; set; }
        public decimal ModificacionInhouse { get; set; }
        public decimal PyActualInHouse { get; set; }
        public decimal ActualRealInHouse { get; set; }

    }

    public class ReporteResumenMontosDetalleFinalDTO
    {
        public string Anterior { get; set; }
        public string Actual { get; set; }
        public string TipoMonto { get; set; }//programa
        public string Periodo { get; set; }//periodoporfechavencimiento
        public string Monto { get; set; }//total dolares
    }

    public class ReporteResumenMontosGeneralTotalDTO
    {
        public List<ReporteResumenMontosDTO> ResumenMontos { get; set; }
        public List<ReporteResumenMontosCierreDTO> ResumenMontosCierre { get; set; }
        public List<ReporteResumenMontosNuevosMatriculadosDTO> ResumenNuevosMatriculados { get; set; }
        public List<ReporteResumenMontosCambiosDTO> Cambios { get; set; }
        public List<ReporteResumenMontosDiferenciasDTO> Diferencias { get; set; }
    }
    public class ReporteResumenMontosDetalleDTO
    {

        public string Tipo { get; set; }
        public List<ReporteResumenMontosDetallesMesesDTO> ListaMontosMeses { get; set; }
    }
    public class ReporteResumenMontosDetallesMesesDTO
    {
        public string Mes { get; set; }
        public string Monto { get; set; }
        public string Diferencia { get; set; }

    }
    public class ReporteResumenMontos
    {
        public string g { get; set; }
        public List<ReporteResumenMontosDetalleFinalDTO> l { get; set; }
    }

    public class ReporteResumenMontosCierrePeriodoDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
        public decimal Actual { get; set; }
        public decimal MontoPagado { get; set; }

    }

    public class ReporteResumenMontosNuevosMatriculadosDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
        public int TipoFecha { get; set; } // 0 = Vencimiento , 1= Pago
        public decimal PagadoD { get; set; }

    }

    public class ReporteResumenMontosVariacionesDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
        public decimal ActualMontos { get; set; }
        public decimal ActualCierre { get; set; }
        public decimal MontoPagadoMontos { get; set; }
        public decimal MontoPagadoCierre { get; set; }
        public decimal DiferenciaPorModificacion { get; set; }
        public int NuevaConsultoria { get; set; }
        public decimal NuevasMatriculas { get; set; }
        public decimal IngresoRealNuevasMatriculas { get; set; }
        public decimal PendientMesOrdenServicio { get; set; }
        public decimal PendientMesSinOrdenServicio { get; set; }
        public decimal RetirosCD_Mes { get; set; }
        public decimal RetirosSD_Mes { get; set; }
        public decimal IncrementosDisminucionesCronograma { get; set; }
        public decimal ModificacionInhouse { get; set; }
    }

    public class ReporteResumenMontosUnionCierreDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
        public decimal Actual { get; set; }
        public decimal MontoPagado { get; set; }

    }

    public class ReporteResumenMontosPagosDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
        public int IdTipoModalidad { get; set; }
        public decimal Actual { get; set; }
        public decimal MontoPagado { get; set; }

        public decimal Proyectado { get; set; }
        public decimal Retiro_CD { get; set; }
        public decimal Retiro_SD { get; set; }

        public decimal DiferenciaCambioFecha { get; set; }
        public decimal DiferenciaCambioMonto { get; set; }
        public decimal DiferenciaConsiderarMoraAdelantoSgteCuota { get; set; }
        public decimal DiferenciaModificacionNroCuotas { get; set; }


        public decimal DiferenciaPorModificacion { get; set; }
        public int NuevaConsultoria { get; set; }
        public decimal NuevasMatriculas { get; set; }
        public decimal IngresoRealNuevasMatriculas { get; set; }
        public decimal IngresoRealNuevasMatriculasFechaPago { get; set; }
        public decimal PendientMesOrdenServicio { get; set; }
        public decimal PendientMesSinOrdenServicio { get; set; }
        public decimal RetirosCD_Mes { get; set; }
        public decimal RetirosSD_Mes { get; set; }
        public decimal IncrementosDisminucionesCronograma { get; set; }
        public decimal ModificacionInhouse { get; set; }

    }

    public class ReporteResumenMontosModalidadesDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
        public int IdTipoModalidad { get; set; }
        public decimal Actual { get; set; }
        public decimal MontoPagado { get; set; }

        public decimal Proyectado { get; set; }
        public decimal Retiro_CD { get; set; }
        public decimal Retiro_SD { get; set; }

        public decimal DiferenciaCambioFecha { get; set; }
        public decimal DiferenciaCambioMonto { get; set; }
        public decimal DiferenciaConsiderarMoraAdelantoSgteCuota { get; set; }
        public decimal DiferenciaModificacionNroCuotas { get; set; }
    }
}
