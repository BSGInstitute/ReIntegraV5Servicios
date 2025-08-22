namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FurPorPagarDTO
    {
        public int IdFur { get; set; }
        public string Empresa { get; set; }
        public string Sede { get; set; }
        public string Area { get; set; }
        public string TipoPedido { get; set; }
        public string CodigoFur { get; set; }
        public string ProductoServicio { get; set; }
        public decimal Cantidad { get; set; }
        public string Unidades { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string MonedaProveedor { get; set; }
        public string Descripcion { get; set; }
        public string CentroCosto { get; set; }
        public string Atipico { get; set; }
        public string Rubro { get; set; }
        public long NroCuenta { get; set; }
        public string Cuenta { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string RUC { get; set; }
        public string Proveedor { get; set; }
        public string MonedaReal { get; set; }
        public decimal MontoAPagar { get; set; }
        public decimal MontoAPagarDolares { get; set; }
        public DateTime FechaProgramada { get; set; }
        public string MesProgramado { get; set; }
        public string Estado { get; set; }
    }

    public class FiltroFurPorPagarDTO
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
