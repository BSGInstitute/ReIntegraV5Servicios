using AutoMapper.Configuration.Conventions;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CronogramaPagoDetalleFinalDTO
    {
        public int Id { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int? IdEstadoMatricula { get; set; }
        public int? IdSubEstadoMatricula { get; set; }
        public int? NroCuota { get; set; }
        public int? NroSubCuota { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal? TotalPagar { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? Saldo { get; set; }
        public decimal? Mora { get; set; }
        public decimal? MoraCalculada { get; set; }
        public decimal? MontoPagado { get; set; }
        public bool? Cancelado { get; set; }
        public string? TipoCuota { get; set; }
        public string? Moneda { get; set; }
        public DateTime? FechaPago { get; set; }
        public int? IdFormaPago { get; set; }
        public string NombreFormaPago { get; set; }
        public int? IdCuenta { get; set; }
        public DateTime? FechaPagoBanco { get; set; }
        public bool? Enviado { get; set; }
        public string? Observaciones { get; set; }
        public int? IdDocumentoPago { get; set; }
        public string? NroDocumento { get; set; }
        public string? MonedaPago { get; set; }
        public decimal? TipoCambio { get; set; }
        public decimal? CuotaDolares { get; set; }
        public DateTime? FechaProcesoPago { get; set; }
        public int? Version { get; set; }
        public bool? Aprobado { get; set; }
        public DateTime? FechaDeposito { get; set; }
        public int? WebMoneda { get; set; }
        public double? WebTipoCambio { get; set; }
        public decimal? MoraTarifario { get; set; }
        public DateTime? FechaCompromiso { get; set; }
        public int? VersionCompromiso { get; set; }
        public decimal? MontoCompromiso { get; set; }
        public DateTime? FechaGeneradoCompromiso { get; set;}
        public DateTime? FechaProcesoPagoReal { get; set; }
        public DateTime? FechaIngresoEnCuenta { get; set; }
        public DateTime? FechaEfectivoDisponible { get; set; }
        public DateTime? FechaCompromiso1 { get; set; }
        public DateTime? FechaCompromiso2 { get; set; }
        public DateTime? FechaCompromiso3 { get; set; }
        public DateTime? FechaGeneracionCompromiso1 { get; set; }
        public DateTime? FechaGeneracionCompromiso2 { get; set; }
        public DateTime? FechaGeneracionCompromiso3 { get; set; }
        public int? IdPersonal_CoordinadorCobranza { get; set; }
        public string? UsuarioCoordinadorAcademico { get; set; }
        public string? MonedaMoraTarifario { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class CronogramaPagoDetalleFinalComboDTO
    {
        public int Id { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int? NroCuota { get; set; }
        public int? NroSubCuota { get; set; }
    }
    public class CronogramaPagoDetalleFinalCuotaDTO
    {
        public DateTime? FechaVencimiento { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? Mora { get; set; }
        public int? NroCuota { get; set; }
        public string? Moneda { get; set; }
        public bool? Cancelado { get; set; }
        public decimal MontoCuotaDescuento { get; set; }
    }
    public class PeriodoDuracionProgramaEspecificoDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public int DuracionTotalAproximadaMeses { get; set; }
        public DateTime FechaAproximadaCertificacion { get; set; }
    }
    public class ConjuntoSesionProgramaEspecificoDTO
    {
        public int IdPEspecifico { get; set; }
        public string NombrePEspecifico { get; set; }
        public DateTime FechaSesion { get; set; }
        public string HorarioSesion { get; set; }
        public int DuracionSesionHoras { get; set; }
    }
    public class ConjuntoSesionProgramaEspecificoMaestroDTO
    {
        public int IdPEspecifico { get; set; }
        public string NombrePEspecifico { get; set; }
        public List<ConjuntoSesionProgramaEspecificoDetalleDTO> Sesiones { get; set; }
    }
    public class ConjuntoSesionProgramaEspecificoDetalleDTO
    {
        public DateTime FechaSesion { get; set; }
        public string HorarioSesion { get; set; }
        public int DuracionSesionHoras { get; set; }
    }
    public class CronogramaPagoDetalleFinalFinanzasDTO
    {
        public int Id { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int? IdEstadoMatricula { get; set; }
        public int? IdSubEstadoMatricula { get; set; }
        public int? NroCuota { get; set; }
        public int? NroSubCuota { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal? TotalPagar { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? Saldo { get; set; }
        public decimal? Mora { get; set; }
        public decimal? MontoPagado { get; set; }
        public bool? Cancelado { get; set; }
        public string TipoCuota { get; set; }
        public string Moneda { get; set; }
        public DateTime? FechaPago { get; set; }
        public int? IdFormaPago { get; set; }
        public string NombreFormaPago { get; set; }
        public int? IdCuenta { get; set; }
        public DateTime? FechaPagoBanco { get; set; }
        public bool? Enviado { get; set; }
        public string Observaciones { get; set; }
        public int? IdDocumentoPago { get; set; }
        public string NroDocumento { get; set; }
        public string MonedaPago { get; set; }
        public decimal? TipoCambio { get; set; }
        public decimal? CuotaDolares { get; set; }
        public DateTime? FechaProcesoPago { get; set; }
        public int? Version { get; set; }
        public bool? Aprobado { get; set; }
        public DateTime? FechaDeposito { get; set; }
        public int? WebMoneda { get; set; }
        public double? WebTipoCambio { get; set; }
        public decimal? MoraTarifario { get; set; }
        public DateTime? FechaCompromiso { get; set; }
        public int? VersionCompromiso { get; set; }
        public decimal? MontoCompromiso { get; set; }
        public DateTime? FechaGeneradoCompromiso { get; set; }
    }
    public class PagoActualizadoFechaDTO
    {
        public int IdCuota { get; set; }
        public DateTime? FechaPago { get; set; }
        public string Usuario { get; set; }
    }

    public class PagoActualizadoMoraTarifarioDTO
    {
        public int IdCuota { get; set; }
        public decimal MoraTarifario { get; set; }
        public string Usuario { get; set; }
    }

    public class CuotaDataAdicionalDTO
    {
        public int IdCuota { get; set; }
        public decimal Cuota { get; set; }
        public decimal MoraCalculada { get; set; }
    }

    public class MoraActualizadoDTO
    {
        public List<CronogramaFinalModificadoDTO> ListaCronograma { get; set; }
        public AdelantoMoraDTO Objeto { get; set; }
        public string Usuario { get; set; }
    }
    public class AdelantoMoraDTO
    {
        public string CodigoMatricula { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public decimal Cuota { get; set; }
        public decimal Mora { get; set; }
        public int version { get; set; }
        public int Id { get; set; }
        public decimal MontoAdelanto { get; set; }
    }
    public class ListadoCuotasModificadasDTO
    {
        public string CodigoEspecial { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public decimal Cuota { get; set; }
        public string FechaVencimiento { get; set; }
        public string FechaAnterior { get; set; }
        public bool Enviado { get; set; }
    }

    public class ListaEnterosDTO
    {
        public List<int> ListaEnteros { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaDiferida { get; set; }
        public int Tipo { get; set; }
    }

    public class FechaCronogramaDTO
    {
        public DateTime FechaDiferida { get; set; }
        public int idCronogramaPagoDetalleFinal { get; set; }
        public string UsuarioModificacion { get; set; }
        public int Tipo { get; set; }
    }

    public class FiltroDetalleCuotasTransaccionAuditoriaDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public DateTime FechaPago { get; set; }
        public string FechaPagoFormateado
        {
            get { return FechaPago.ToString("MM/dd/yyyy"); }
        }
    }

    public class DetalleCuotasTransaccionAuditoriaDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdCronogramaPagoDetalleFinal { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public int IdFormaPago { get; set; }
        public string Proveedor { get; set; }
        public string DescripcionPago { get; set; }
        public string EstadoProceso { get; set; }
        public string CodigoTransaccion { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime? FechaOperacion { get; set; }
    }

    public class FiltroDetalleMatriculaTransaccionAuditoriaDTO
    {
        public int IdMontoPagoCronograma { get; set; }
        public int NroCuota { get; set; }
    }

    public class DetalleMatriculaTransaccionAuditoriaDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdMontoPagoCronograma { get; set; }
        public int IdCronogramaPagoDetalleFinal { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public int IdFormaPago { get; set; }
        public string Proveedor { get; set; }
        public string DescripcionPago { get; set; }
        public string EstadoProceso { get; set; }
        public string CodigoTransaccion { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime? FechaOperacion { get; set; }
    }

}
