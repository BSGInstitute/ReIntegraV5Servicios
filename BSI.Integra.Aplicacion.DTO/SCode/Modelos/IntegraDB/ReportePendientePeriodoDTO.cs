using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReportePendientePeriodoDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
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
        public decimal ProyectadoInicial { get; set; }
        public decimal Modificacion { get; set; }
        public decimal RetiradosInicial { get; set; }

    }
    public class ReportePendientePeriodoPorCoordinadorDTO
    {
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
        public decimal MontoPagado { get; set; }
        public decimal IngresoVentas { get; set; }
        public decimal PendientePorFactura { get; set; }
        public decimal PendienteSinFactura { get; set; }
        public decimal MontoVencido { get; set; }
        public decimal MontoPorVencer { get; set; }
        public decimal PagoPrevio { get; set; }
        public decimal PagoDentroMes { get; set; }
        public decimal ProyectadoInicial { get; set; }
        public decimal Modificacion { get; set; }
        public decimal RetiradosInicial { get; set; }
    }
    public class ReportePendienteCompuestoDTO
    {
        public List<ReportePendienteDTO> ReportePendientePorPeriodo { get; set; }
        public List<ReportePendienteDTO> ReportePendienteIngresoVentasPorPeriodo { get; set; }
        public List<ReportePendienteDTO> ReportePendientePorCoordinador { get; set; }
        public List<ReportePendientePorCoordinadorDTO> ReportePendientePeriodoYCoordinador { get; set; }
        public List<ReportePendienteDetallesDTO> ReportePendienteDetalles { get; set; }
        public List<PersonalAsignadoReportePendienteDTO> ListaCoordinadoras { get; set; }
        public string EstadoPersonal { get; set; }
    }
    public class ReportePendienteDTO
    {
        public string g { get; set; }
        public List<ReportePendienteDetalleFinalDTO> l { get; set; }
    }
    public class ReportePendienteDetalleFinalDTO
    {
        public string Anterior { get; set; }
        public string Actual { get; set; }
        public string TipoMonto { get; set; }//programa
        public string Periodo { get; set; }//periodoporfechavencimiento
        public string Monto { get; set; }//total dolares
    }
    public class ReportePendientePorCoordinadorDTO
    {
        public string g { get; set; }
        public List<ReportePendienteDetalleFinalPorCoordinadorDTO> l { get; set; }
    }
    public class ReportePendienteDetalleFinalPorCoordinadorDTO
    {
        public string Anterior { get; set; }
        public string Actual { get; set; }
        public string TipoMonto { get; set; }//programa
        public string Coordinador { get; set; }//coordinador
        public string Periodo { get; set; }//periodoporfechavencimiento
        public string Monto { get; set; }//total dolares
    }
    public class ReportePendienteDetallesDTO
    {
        public string Ciudad { get; set; }
        public string Coordinador { get; set; }
        public string Programa { get; set; }
        public string CodigoAlumno { get; set; }
        public string EstadoMatricula { get; set; }
        public string Alumno { get; set; }
        public DateTime FechaCuota { get; set; }
        public decimal MontoCuota { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public decimal Mora { get; set; }
        public int NroCuota { get; set; }
        public string Moneda { get; set; }
        public decimal MontoCuotaDol { get; set; }
        public decimal MontoPagadoDol { get; set; }
        public decimal SaldoPendienteDol { get; set; }
        public string Documentacion { get; set; }
    }
    public class ReportePendienteGeneralDTO
    {
        public List<ReportePendientePeriodoyCoordinadorDTO> Periodo { get; set; }
        public List<ReportePendientesCambiosPorCoordinadorDTO> Cambios { get; set; }
        public List<ReportePendientesDiferenciasDTO> Diferencias { get; set; }
        public List<ReportePendientePeriodoyCoordinadorDTO> Cierre { get; set; }
        public List<ReportePendientesModificacionesMesDTO> ModificacionesMes { get; set; }
        public string FechaCierreActual { get; set; }
        public string FechaCierrePrevio { get; set; }
    }

    public class EnvioDarosPruebaDTO
    {
        public List<ReportePendientePeriodoyCoordinadorDTO> Periodo { get; set; }
        public List<ReportePendientePeriodoyCoordinadorDTO> PeriodoCoordinador { get; set; }
        public List<ReportePendientePeriodoyCoordinadorDTO> Matriculados { get; set; }
        public DateTime FechaCorte { get; set; }
        public DateTime FechaCortePrevio { get; set; }

    }
    public class ReportePendientePeriodoyCoordinadorDTO
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
        public decimal PendientePorFactura { get; set; }
        public decimal PendienteSinFactura { get; set; }
        public decimal MontoVencido { get; set; }
        public decimal MontoPorVencer { get; set; }
        public decimal PagoPrevio { get; set; }
        public decimal PagoDentroMes { get; set; }
        public decimal PagoEnFechaVenc { get; set; }
        public decimal MatriculadosFechaPago { get; set; }
        public decimal MatriculadosFechaVencimiento { get; set; }
        public decimal ProyectadoInicial { get; set; }
        public decimal Modificacion { get; set; }
        public decimal RetiradosInicial { get; set; }
    }
    public class ReportePendientesCambiosPorCoordinadorDTO
    {
        public string IdMatricula { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public decimal MontoProyectado { get; set; }
        public decimal MontoActual { get; set; }
        public decimal TipoCambio { get; set; }
        public string PeriodoProyectado { get; set; }
        public string PeriodoActual { get; set; }
        public decimal Diferencia { get; set; }
        public string Cambio { get; set; }
        public string Coordinador { get; set; }
    }
    public class ReportePendientesDiferenciasDTO
    {
        public string IdMatricula { get; set; }
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
    public class ReportePendientesModificacionesMesDTO
    {
        public string IdMatricula { get; set; }
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
        public string PeriodoCambio { get; set; }
    }
    public class ReportePendienteFiltroDTO
    {
        public DateTime PeriodoInicio { get; set; }
        public DateTime PeriodoFin { get; set; }
        public string PeriodoCierre { get; set; }
        public List<int> Modalidad { get; set; }
        public List<string> Coordinadora { get; set; }
        public string EstadoPersonal { get; set; }
    }
    public class ReportePendienteDetalleDTO
    {
        public string Tipo { get; set; }
        public List<ReportePendienteDetallesMesesDTO> ListaMontosMeses { get; set; }
    }
    public class ReportePendienteDetallesMesesDTO
    {
        public string Mes { get; set; }
        public string Monto { get; set; }
        public string Diferencia { get; set; }
    }
    public class ReportePendientePeriodoTotalizadoDTO
    {
        public string PeriodoPorFechaVencimiento { get; set; }
        public decimal IngresoVentas { get; set; }
        public decimal PagoEnFechaVenc { get; set; }
    }
    public class ReportePendienteDetallePorCoordinadorDTO
    {
        public string Tipo { get; set; }
        public string Coordinador { get; set; }
        public List<ReportePendienteDetallesMesesDTO> ListaMontosMeses { get; set; }
    }
}
