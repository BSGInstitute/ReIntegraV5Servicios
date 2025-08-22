namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FurPagoDTO
    {
        public int IdFur { get; set; }
        public string Codigo { get; set; }
        public int? IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string NombreProveedorComprobante { get; set; }
        public int? IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int? IdCC { get; set; }
        public int IdPais { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NumeroCuenta { get; set; }
        public string DescripcionCuenta { get; set; }
        public decimal Cantidad { get; set; }
        public int MonedaFur { get; set; }
        public string NombreMonedaFur { get; set; }
        public decimal PrecioUnitarioSoles { get; set; }
        public decimal PrecioUnitarioDolares { get; set; }
        public decimal PrecioTotalSoles { get; set; }
        public decimal PrecioTotalDolares { get; set; }
        public string NombreDocumento { get; set; }
        public string NumeroRecibo { get; set; }
        public string Descripcion { get; set; }
        public string NumeroComprobante { get; set; }
        public decimal? PagoMonedaOrigen { get; set; }
        public decimal? PagoDolares { get; set; }
        public int FaseAprobacion { get; set; }
        public int? Antiguo { get; set; }
        public int? MonedaPagoRealizado { get; set; }
        public string NombreMonedaPagoRealizado { get; set; }
        public bool EstadoCancelado { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    public class FiltroFurPagoDTO
    {
        public int? area { get; set; }
        public int? ciudad { get; set; }
        public int? anio { get; set; }
        public int? semana { get; set; }
        public int? moneda { get; set; }
        public bool? estado { get; set; } = null;
    }
    public class FurPagoRealizadoDTO
    {
        public int Id { get; set; }
        public int? IdComprobantePagoPorFur { get; set; }
        public string NombreComprobantePagoPorFur { get; set; }
        public int NumeroPago { get; set; }
        public int IdMoneda { get; set; }
        public string NombreMoneda { get; set; }
        public int? NumeroCuenta { get; set; }
        public string NumeroRecibo { get; set; }
        public int IdFormaPago { get; set; }
        public string NombreFormaPago { get; set; }
        public DateTime FechaCobroBanco { get; set; }
        public decimal PrecioTotalMonedaOrigen { get; set; }
        public decimal PrecioTotalMonedaDolares { get; set; }
        public bool IdCancelado { get; set; }
        public string NombreCancelado { get; set; }
    }
    public class RegistrarFurPagoDTO
    {
        public int Id { get; set; }
        public int? IdFur { get; set; }
        public int? IdComprobantePago { get; set; }
        public int NumeroPago { get; set; }
        public int IdMoneda { get; set; }
        public int NumeroCuenta { get; set; }
        public string NumeroRecibo { get; set; }
        public int IdFormaPago { get; set; }
        public DateTime FechaCobroBanco { get; set; }
        public decimal PrecioTotalMonedaOrigen { get; set; }
        public decimal PrecioTotalMonedaDolares { get; set; }
        public string Usuario { get; set; }
        public bool IdCancelado { get; set; }
        public int? IdComprobantePagoPorFur { get; set; }

    }
}
