namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MontoPagoCronogramaDetalleDTO
    {
        public int Id { get; set; }
        public int NumeroCuota { get; set; }
        public double MontoCuota { get; set; }
        public DateTime FechaPago { get; set; }
        public string CuotaDescripcion { get; set; } = null!;
        public double MontoCuotaDescuento { get; set; }
        public bool Pagado { get; set; }
        public int? IdMontoPagoCronograma { get; set; }
        public bool Matricula { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class MontoPagoCronogramaDetalleComboDTO
    {
        public int Id { get; set; }
        public int? IdMontoPagoCronograma { get; set; }
    }
    public class MontoPagoCronogramaDetalleValidoDTO
    {
        public int Id { get; set; }
        public int NumeroCuota { get; set; }
        public string CuotaDescripcion { get; set; }
        public double MontoCuota { get; set; }
        public DateTime FechaPago { get; set; }
        public double MontoCuotaDescuento { get; set; }
        public bool Pagado { get; set; }
        public bool Matricula { get; set; }
        public string Cronograma { get; set; }
        public string RowVersion { get; set; }
    }
    public class CuotaCronogramaDetalleDTO
    {
        public int NroCuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Cuota { get; set; }
        public string Moneda { get; set; }
        public string SimboloMoneda { get; set; }
    }
    public class MontoPagoCronogramaDetalleInterfazDTO
    {
        public int Id { get; set; }
        public int NumeroCuota { get; set; }
        public double MontoCuota { get; set; }
        public DateTime FechaPago { get; set; }
        public string CuotaDescripcion { get; set; } = null!;
        public double MontoCuotaDescuento { get; set; }
        public bool Pagado { get; set; }
        public int? IdMontoPagoCronograma { get; set; }
        public bool Matricula { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ResultadoDTO
    {
        public int Resultado { get; set; }
    }
    public class RespuestaDTO
    {
        public int Respuesta { get; set; }
    }
    public class MontoPagoCronogramaCompletoDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int IdMontoPago { get; set; }
        public int IdPersonal { get; set; }
        public double Precio { get; set; }
        public double PrecioDescuento { get; set; }
        public int IdMoneda { get; set; }
        public int IdTipoDescuento { get; set; }
        public bool EsAprobado { get; set; }
        public string NombrePlural { get; set; }
        public int Formula { get; set; }
        public int MatriculaEnProceso { get; set; }
        public string CodigoMatricula { get; set; }
        public Guid? IdMigracion { get; set; }
        public string TipoPersonal { get; set; }
        public string Usuario { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IdAlumnoPortal { get; set; }
        public string CodigoBancario { get; set; }
        public String SimboloMoneda { get; set; }
        public int IdMedioPago { get; set; }
        public List<TipoDescuentoOportunidadDTO> ListaTipoDescuento { get; set; }
        public List<MontoPagoOportunidadDTO> ListaMontosPagosVentas { get; set; }
        public List<MontoPagoCronogramaDetalleDTO> ListaDetalleCuotas { get; set; }
        public MontoPagoDTO MontoPago { get; set; }
    }
}
