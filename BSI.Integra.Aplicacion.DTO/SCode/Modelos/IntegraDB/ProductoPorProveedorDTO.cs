namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProductoPorProveedorDTO
    {
        public int Id { get; set; }
        public int IdHistoricoProveedorProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int IdMoneda { get; set; }
    }
    public class ProductoPorProveedorUltimaVersionDTO
    {
        public int IdHistoricoProductoProveedor { get; set; }
        public int IdProveedor { get; set; }
        public string NroDocumento { get; set; }
        public string RazonSocial { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal Precio { get; set; }
        public int IdMoneda { get; set; }
    }

    public class DetalleHistoricoFurDTO
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; }
        public string CuentaDescripcion { get; set; }
        public int IdProductoPresentacion { get; set; }
        public decimal PrecioOrigen { get; set; }
        public decimal PrecioDolares { get; set; }
        public decimal Precio { get; set; }
        public int IdMoneda { get; set; }
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
    }

}
