namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CronogramaPagoDTO
    {
        public int Id { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int? IdAlumno { get; set; }
        public int IdPespecifico { get; set; }
        public string? Periodo { get; set; }
        public string? Moneda { get; set; }
        public string? AcuerdoPago { get; set; }
        public double? TipoCambio { get; set; }
        public double? TotalPagar { get; set; }
        public int? NroCuotas { get; set; }
        public DateTime? FechaIniPago { get; set; }
        public bool? Enviado { get; set; }
        public string? Observaciones { get; set; }
        public bool? ConCuotaInicial { get; set; }
        public decimal? CuotaInicial { get; set; }
        public bool? CadaNdias { get; set; }
        public int? Ndias { get; set; }
        public string? WebMoneda { get; set; }
        public double? WebTipoCambio { get; set; }
        public double? WebTotalPagar { get; set; }
        public double? WebTotalPagarConv { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class CronogramaPagoComboDTO
    {
        public int Id { get; set; }
        public int? IdMatriculaCabecera { get; set; }
    }
    public class ProgramaCuotasDTO
    {
        public int? IdBusqueda { get; set; }
        public string? NombreCurso { get; set; }
        public int IdPespecifico { get; set; }
        public string NombrePespecifico { get; set; } = null!;
        public int IdMatricula { get; set; }
        public string CodigoMatricula { get; set; } = null!;
        public string TipoPrograma { get; set; } = null!;
        public string? DuracionPGeneral { get; set; }
        public string? DuracionPespecifico { get; set; }
        public int? NumeroCuotas { get; set; }
        public string? WebMoneda { get; set; }
        public decimal? TotalPagar { get; set; }
        public decimal? WebTotalPagar { get; set; }
        public decimal? WebTipoCambio { get; set; }
        public bool EstadoCronogramaMod { get; set; }
    }
    public class ProgramaCuotasDetalleDTO
    {
        public int? IdBusqueda { get; set; }
        public string? NombreCurso { get; set; }
        public int IdPespecifico { get; set; }
        public string NombrePespecifico { get; set; } = null!;
        public int IdMatricula { get; set; }
        public string CodigoMatricula { get; set; } = null!;
        public string TipoPrograma { get; set; } = null!;
        public string? DuracionPGeneral { get; set; }
        public string? DuracionPespecifico { get; set; }
        public int? NumeroCuotas { get; set; }
        public string? WebMoneda { get; set; }
        public decimal? TotalPagar { get; set; }
        public decimal? WebTotalPagar { get; set; }
        public decimal? WebTipoCambio { get; set; }
        public bool EstadoCronogramaMod { get; set; }
        public List<CronogramaPagoDetalleFinalCuotaDTO> Cuotas { get; set; } = new List<CronogramaPagoDetalleFinalCuotaDTO>();
    }

    public class ComprobanteRecienteDTO
    {
        public int IdTipoComprobante { get; set; } = -1;
        public string? NroDocumentoComprobante { get; set; }
        public string? NombreRazonSocial { get; set; }
    }

    public class CronogramaPagoAlumnoDTO
    {
        public List<CronogramaDetallePagoDTO> ListaCronogramaDetallePago { get; set; }
        public string Moneda { get; set; }
        public decimal TipoCambio { get; set; }
        public int IdPEspecifico { get; set; }
        public int IdAlumno { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class CronogramaDetallePagoDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }// ver
        public int NroCuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal TotalPagar { get; set; }
        public decimal Cuota { get; set; }
        public decimal Saldo { get; set; }
        public string TipoCuota { get; set; }
        public string Moneda { get; set; }

    }

     

    public partial class CronogramaPagoDetalleDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string? CodigoMatricula { get; set; }
        public int IdAlumno { get; set; }
        public string? PGeneral { get; set; }
        public int IdBusqueda { get; set; }
        public int IdPGeneral { get; set; }
        public int IdPEspecifico { get; set; }
        public int CuotasPagadas { get; set; }
        public int CuotasPendientes { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public List<ListaCuotaPagoDTO> RegistroCuota { get; set; }
    }
}
