using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReportePagosDiaPeriodoDTO
    {
        public DateTime? FechaPagoDia { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string PeriodoFechaPagoDia { get; set; }
        public string PeriodoFechaVencimiento { get; set; }
        public string Actual { get; set; }
        public string MontoPagado { get; set; }
        public string MontoPendiente { get; set; }
        public string ActualConAtrasos { get; set; }
        public string ActualSinAtrasos { get; set; }
        public string TotalPagadoDentroDelMes { get; set; }
    }
    public class ReportePagosDiaPeriodoCompuestoDTO
    {
        public IEnumerable<ReportePagosDiaPeriodoAgrupadoDTO> ReportePagosPorDia { get; set; }
        public IEnumerable<ReportePagosDiaPeriodoAgrupadoDTO> ReportePagosPorPeriodo { get; set; }
    }
    public class ReportePagosDiaPeriodoAgrupadoDTO
    {
        public string Periodo { get; set; }
        public List<ReportePagosDiaPeriodoAgrupadoDetalleFechaDTO> DetalleFecha { get; set; }
    }
    public class ReportePagosDiaPeriodoAgrupadoDetalleFechaDTO
    {
        public string FechaVencimiento { get; set; }
        public string PeriodoFechaVencimiento { get; set; }
        public string Actual { get; set; }
        public string MontoPagado { get; set; }
        public string MontoPendiente { get; set; }
        public string ActualConAtrasos { get; set; }
        public string ActualSinAtrasos { get; set; }
        public string TotalPagadoDentroDelMes { get; set; }
    }
    public class PagosDiaPeriodoGeneralDTO
    {
        public List<ReportePagosDiaPeriodoDTO> Periodo { get; set; }
        public List<ReportePagosDiaPeriodoDTO> PeriodoMeses { get; set; }
    }
    public class ReportePagosDiaPeriodoFiltroDTO
    {
        public int Periodo { get; set; }
        public List<string> Coordinadora { get; set; }
    }
    public class CombosPagosDiaPeriodoDTO
    {
        public List<FiltroDTO> ListaPeriodo { get; set; }
        public List<DatoPersonalCoordinadorDTO> ListaCoordinador { get; set; }
        public List<PersonalAsignadoDTO> AsistentesActivos { get; set; }
        public List<PersonalAsignadoDTO> AsistentesInactivos { get; set; }
        public List<PersonalAsignadoDTO> AsistentesTotales { get; set; }
    }
    public class ReporteEstadosAlumnosDTO
    {
        public IEnumerable<ReporteEstadoAlumnosEstadoSubEstadoAgrupadoDTO> ReporteAvanceAcademicoPagos { get; set; }
        public IEnumerable<ReporteEstadoAlumnosAgrupadoDTO> ReporteAvanceAcademicoAonline { get; set; }
        public IEnumerable<ReporteEstadoAlumnosAvanceAcademicoVSPagosAgrupadoDTO> ReporteAvanceAcademicoVSPagosAonline { get; set; }
        public IEnumerable<ReporteEstadoAlumnosPagosAtrasadosAgrupadoDTO> ReporteAvanceAcademicoAlumnosPagoAtrasado { get; set; }
    }
    public class ReporteEstadoAlumnosEstadoSubEstadoAgrupadoDTO
    {
        public string Coordinadora { get; set; }
        public List<ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO> Detalle { get; set; }
    }
    public class ReporteAvanceAcademicoEstadoSubestadoPresencialOnlineAonlineDTO
    {
        public string Coordinadora { get; set; }
        public string Tipo { get; set; }
        public string EstadoMatricula { get; set; }
        public string SubEstadoMatricula { get; set; }
        public int Total { get; set; }
    }
    public class ReporteEstadoAlumnosAgrupadoDTO
    {
        public string Coordinadora { get; set; }
        public List<ReporteAvanceAcademicoPresencialOnlineDTO> Detalle { get; set; }
    }
    public class ReporteAvanceAcademicoPresencialOnlineDTO
    {
        public string Coordinadora { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public int Total { get; set; }
    }
    public class ReporteEstadoAlumnosAvanceAcademicoVSPagosAgrupadoDTO
    {
        public string Coordinadora { get; set; }
        public List<ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO> Detalle { get; set; }
    }
    public class ReporteAvanceAcademicoAvanceAcademicoVSPagosDTO
    {
        public string Coordinadora { get; set; }
        public string Tipo { get; set; }
        public string EstadoAcademico { get; set; }
        public string EstadoPagos { get; set; }
        public int Total { get; set; }
    }
    public class ReporteEstadoAlumnosPagosAtrasadosAgrupadoDTO
    {
        public string Coordinadora { get; set; }
        public List<ReporteAvanceAcademicoAlumnosPagosAtrasados> Detalle { get; set; }
    }
    public class ReporteAvanceAcademicoAlumnosPagosAtrasados
    {
        public string Coordinadora { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public int NumeroAlumnos { get; set; }
        public int NumeroCuotasAtrasadas { get; set; }
        public decimal MontoTotalCuotasAtrasadas { get; set; }
    }
}
