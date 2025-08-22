namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReportePagosACuentaDTO
    {
        public string NroCuenta { get; set; }
        public string Cuenta { get; set; }
        public string Rubro { get; set; }
        public int AnioPago { get; set; }
        public string MesPago { get; set; }
        public int AnioPagoEjecucion { get; set; }
        public string MesPagoEjecucion { get; set; }
        public int IdMoneda { get; set; }
        public string Moneda { get; set; }
        public decimal MontoPagadoOriginal { get; set; }
        public decimal MontoPagadoDolar { get; set; }
    }

    public class ReportePagosACuentaGeneralDTO
    {
        public List<ReportePrincipalDTO> ListaPrincipal { get; set; }
        public List<AnioAgrupadoDTO> Anios { get; set; }
    }

    public class AnioAgrupadoDTO
    {
        public int AnioPago { get; set; }
        public List<PeriodoAgrupadoDTO> Periodos { get; set; }
    }

    public class PeriodoAgrupadoDTO
    {
        public string MesPago { get; set; }
        public List<PeriodoPagoCuentaDTO> Detalles { get; set; }
    }


    public class PeriodoPagoCuentaDTO
    {
        public string Cuenta { get; set; }
        public string NroCuenta { get; set; }
        public string Periodo { get; set; }
        public decimal MontoPago { get; set; }
    }

    public class ReportePrincipalDTO
    {
        public string Rubro { get; set; }
        public string NroCuenta { get; set; }
        public string Cuenta { get; set; }
    }
    public class ReportePagosACuentaDetalleDTO
    {
        public int AnioPago { get; set; }
        public List<ReportePrincipalDTO> ListaPrincipal { get; set; }
        public List<PeriodoAgrupadoDeudaDTO> Periodos { get; set; }
    }

    public class ReportePagosFinalDTO {
        public ReportePagosACuentaGeneralDTO ReporteGeneral { get; set; }
        public List<ReportePagosACuentaDetalleDTO> ReporteDetallado { get; set; }
    }

    public class PeriodoAgrupadoDeudaDTO
    {
        public string Periodo { get; set; }
        public List<AnioDeudaDTO> AniosDeuda { get; set; }
    }
    public class AnioDeudaDTO
    {
        public int AnioDeuda { get; set; }
        public List<PeriodoDeudaDTO> PeriodosDeuda { get; set; }
    }

    public class PeriodoDeudaDTO
    {
        public string MesDeuda { get; set; }
        public List<DetalleDeudaDTO> DetallesDeuda { get; set; }
    }

    public class DetalleDeudaDTO
    {
        public string NroCuenta { get; set; }
        public string Cuenta { get; set; }
        public string PeriodoDeuda { get; set; }
        public decimal MontoDeuda { get; set; }
    }

    public class TasaCambioReportePagosDTO
    {
        public string Periodo { get; set; }
        public string Moneda { get; set; }
        public bool Existe { get; set; }
    }
}
