namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReportePagosDTO
    {
        public int Id { get; set; }
        public string Empresa { get; set; }
        public string EmpresaFur { get; set; }
        public string Sede { get; set; }
        public string Area { get; set; }
        public string TipoPedido { get; set; }
        public string CodigoFur { get; set; }
        public string ProductoServicio { get; set; }
        public decimal Cantidad { get; set; }
        public string Unidades { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string Moneda { get; set; }
        public string Descripcion { get; set; }
        public string CentroCosto { get; set; }
        public string Curso { get; set; }
        public string Programa { get; set; }
        public string Rubro { get; set; }
        public string NroCuenta { get; set; }
        public string Cuenta { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string TipoDocumento { get; set; }
        public string NroDoc { get; set; }
        public string Proveedor { get; set; }
        public string TipoComprobante { get; set; }
        public string NumComprobante { get; set; }
        public string MonedaPago { get; set; }
        public decimal? MontoPagado { get; set; }
        public decimal? TotalDolares { get; set; }
        public DateTime? FechaEmisionComprobante { get; set; }
        public string NumCuenta { get; set; }
        public string NumRecibo { get; set; }
        public string TipoPago { get; set; }
        public DateTime? FechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime FechaPagoBanco { get; set; }
        public string MesPagoBanco { get; set; }
        public string Anterior { get; set; }
        public decimal? MontoProgramado { get; set; }
        public decimal? MontoNoProgramado { get; set; }
        public string UsuarioAprobacion { get; set; }
        public string MesPresupuesto { get; set; }
        public int AnioPresupuesto { get; set; }
        public string MesVcto { get;set; }
        public int? AnioVcto { get; set; }
        public string Observaciones { get; set; }
    }

    public class ReporteDocumentosPendientesPagoDTO
    {
        public int Id { get; set; }
        public string Empresa { get; set; }
        public string EmpresaFur { get; set; }
        public string Sede { get; set; }
        public string Area { get; set; }
        public string TipoPedido { get; set; }
        public string CodigoFur { get; set; }
        public string ProductoServicio { get; set; }
        public decimal Cantidad { get; set; }
        public string Unidades { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string Moneda { get; set; }
        public string Descripcion { get; set; }
        public string CentroCosto { get; set; }
        public string Curso { get; set; }
        public string Programa { get; set; }
        public string Atipico { get; set; }
        public string Rubro { get; set; }
        public string NroCuenta { get; set; }
        public string Cuenta { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string TipoDocumento { get; set; }
        public string NroDoc { get; set; }
        public string Proveedor { get; set; }
        public string TipoComprobante { get; set; }
        public string NumComprobante { get; set; }
        public string MonedaComprobante { get; set; }
        public decimal? MontoaPagar { get; set; }
        public decimal? TotalDolaresAsociado { get; set; }
        public string MonedaPago { get; set; }
        public decimal? ACuenta { get; set; }
        public decimal? TotalDolaresACuenta { get; set; }
        public decimal? TotalDolaresPendiente { get; set; }
        public DateTime FechaEmisionComprobante { get; set; }
        public string MesdeEmision { get; set; }
        public DateTime FechaVencimientoComprobante { get; set; }
        public string MesdeVcto { get; set; }
        public string Estado { get; set; }
        public string Anterior { get; set; }
        public string UsuarioAprobacion { get; set; }
        public string MesPresupuesto { get; set; }
        public int AnioPresupuesto { get; set; }
        public int AnioVcto { get; set; }
        public string  Observaciones { get; set; }
    }

}
