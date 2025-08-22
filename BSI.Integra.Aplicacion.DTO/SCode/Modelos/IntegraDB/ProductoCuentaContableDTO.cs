namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProductoCuentaContableDTO
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = null!;
        public string DescripcionProducto { get; set; } = null!;
        public long CuentaEspecifica { get; set; }
        public int IdProductoPresentacion { get; set; }
    }

}
