namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReportePresupuestoDTO
    {
        public int IdFur { get; set; }
        public string Empresa { get; set; }
        public string EmpresaFur { get; set; }
        public string Sede { get; set; }
        public string Area { get; set; }
        public string TipoPedido { get; set; }
        public string CodigoFur { get; set; }
        public string ProductoServicio { get; set; }
        public string Descripcion { get; set; }
        public string CentroCosto { get; set; }
        public string Curso { get; set; }
        public string Programa { get; set; }
        public string MonedaProyectada { get; set; }
        public decimal? CantidadProyectada { get; set; }
        public string PresentacionProyectada { get; set; }
        public decimal? PrecioUnitarioProyectado { get; set; }
        public decimal? PrecioTotalOrigenProyectado { get; set; }
        public decimal? PrecioTotalDolaresProyectado { get; set; }
        public string MonedaProveedor { get; set; }
        public decimal? Cantidad { get; set; }
        public string Unidad { get; set; }
        public decimal? PrecioUnitarioJF { get; set; }
        public decimal? PrecioTotalOrigenJF { get; set; }
        public decimal? PrecioTotalDolaresJF { get; set; }
        public string Atipico { get; set; }
        public string Rubro { get; set; }
        public long? NroCuenta { get; set; }
        public string Cuenta { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioAprobacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string RUC { get; set; }
        public string Proveedor { get; set; }
        public int? IdComprobantePago { get; set; }
        public string TipoComprobante { get; set; }
        public string NumeroComprobante { get; set; }
        public string MonedaComprobante { get; set; }
        public decimal? MontoPorPagar { get; set; }
        public decimal? MontoPorPagarDolares { get; set; }
        public DateTime? FechaEmisionComprobante { get; set; } = null;
        public string MesEmision { get; set; }
        public DateTime? FechaVencimientoComprobante { get; set; } = null;
        public string MesVencimiento { get; set; }
        public string MonedaPago { get; set; }
        public decimal? MontoRealPagado { get; set; }
        public decimal? MontoRealPagadoDolares { get; set; }
        public string NumeroReciboBanco { get; set; }
        public string EntidadFinanciera { get; set; }
        public string CuentaCorriente { get; set; }
        public DateTime? FechaPagoBanco { get; set; } = null;
        public string MesPagoBanco { get; set; }
        public DateTime? FechaProgramacionOriginal { get; set; } = null;
        public string MesProgramacionOriginal { get; set; }
        public DateTime? FechaProgramacionActual { get; set; } = null;
        public string MesProgramacionActual { get; set; }
        public string FaseAprobacion { get; set; }
        public string SubFaseFur { get; set; }
        public string EstadoComprobante { get; set; }


        public DateTime? FechaLimiteFur { get; set; } = null;
        public string MesLimiteFur { get; set; }
        public string FormaPago { get; set; }
        public string EsDiferido { get; set; }


    }

    public class FiltroPresupuestoDTO
    {
        public int? idCentroCosto { get; set; }
        public int? idFur { get; set; }
        public int? idCuenta { get; set; }
        public int? idEstadoFur { get; set; }
        public string? idsPeridoFechaLimite { get; set; }
        public int? idPeridoProgramacionActual { get; set; }
        public string? idsPeridoProgramacionOriginal { get; set; }
        public int? idProveedor { get; set; }
        public int? idRubro { get; set; }
        public int? idSede { get; set; }
        public int? idTipoPedido { get; set; }
        public int? idUsarioCreacion { get; set; }
    }

    public class DiferirFurDTO
    {
        public string IdsFur { get; set; }
        public bool EsDiferido { get; set; }
        public DateTime? FechaLimiteReprogramacion { get; set; }
        public string? Usuario { get; set; }
    }
}
