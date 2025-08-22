namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class HistoricoProductoProveedorVersionDTO
    {
        public int Id { get; set; }
        public string Producto { get; set; } = null!;
        public int IdProducto { get; set; }
        public string Proveedor { get; set; } = null!;
        public int IdProveedor { get; set; }
        public int? IdCondicionPago { get; set; }
        public string CondicionPago { get; set; } = null!;
        public string Moneda { get; set; } = null!;
        public int IdMoneda { get; set; }
        public decimal Precio { get; set; }
        public int IdTipoPago { get; set; }
        public string TipoPago { get; set; } = null!;
        public string Observaciones { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
    }
}
