namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class RegistroPreProcesoPagoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPasarelaPago { get; set; }
        public int IdAlumno { get; set; }
        public int IdFormaPago { get; set; }
        public string? MedioPago { get; set; }
        public string? MedioCodigo { get; set; }
        public string? WebMoneda { get; set; }
        public RegistroPreDatoAlumnoDTO? RegistroAlumno { get; set; }
        public List<RegistroPreProcesoPagoCuotaDTO> ListaCuota { get; set; } = new List<RegistroPreProcesoPagoCuotaDTO>();
    }
    public class RegistroPreProcesoPagoCuotaDTO
    {
        public int IdCuota { get; set; }
        public int NroCuota { get; set; }
        public string? TipoCuota { get; set; }
        public decimal Cuota { get; set; }
        public decimal Mora { get; set; }
        public decimal MoraCalculada { get; set; }
        public decimal CuotaTotal { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string Nombre { get; set; }
        public decimal GestionCobranza { get; set; }
    }
    public class RegistroPreDatoAlumnoDTO
    {
        public int IdAlumno { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Ciudad { get; set; }
        public string? Direccion { get; set; }
        public string? Correo { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? NumeroCelular { get; set; }
    }
    public class RespuestaRegistroPreProcesoPagoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public Guid? IdentificadorTransaccion { get; set; }
        public string? MedioCodigo { get; set; }
        public bool RequiereDatosTarjeta { get; set; }
        public int IdTransaccionAuditoriaPago { get; set; }
    }
    public class MedioPagoActivoPasarelaDTO
    {
        public int IdPasarelaPago { get; set; }
        public string? PasarelaNombre { get; set; }
        public string? MedioPago { get; set; }
        public string? MedioCodigo { get; set; }
        public string? Imagen { get; set; }
        public int IdFormaPago { get; set; }
        public bool DatosCapturados { get; set; }
    }
    public class RegistroProcesoPagoDTO
    {
        public string? IdUsuario { get; set; }
        public int IdAlumno { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public Guid? IdentificadorTransaccion { get; set; }
        public int IdFormaPago { get; set; }
        public string? NumeroPedidoOrden { get; set; }
        public string? TokenComercio { get; set; }
        public string? NombreServicio { get; set; }
        public string? EstadoOperacion { get; set; }
        public string? TipoPago { get; set; }
        public string? CodigoComercio { get; set; }
        public string? RegistroEnvioComercio { get; set; }
        public string? RegistroRespuestaComercio { get; set; }
        public string? RegistroTransaccionJson { get; set; }
        public decimal? MontoTotal { get; set; }
        public string? Correo { get; set; }
        public string? NumeroPedidoComercio { get; set; }
        public bool RequiereDatoTarjeta { get; set; }
        public bool PagoOrganico { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
    public class RegistroTransactionDTO
    {
        public Guid? IdentificadorTransaccion { get; set; }
        public int IdTransaccionAuditoriaPago { get; set; }
        public bool Estado { get; set; }
    }
}
