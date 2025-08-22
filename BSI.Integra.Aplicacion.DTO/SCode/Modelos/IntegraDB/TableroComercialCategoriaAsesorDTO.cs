namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TableroComercialCategoriaAsesorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal MontoVenta { get; set; }
        public int IdMoneda_Venta { get; set; }
        public int IdTableroComercialUnidad_Venta { get; set; }
        public decimal MontoPremio { get; set; }
        public int IdMoneda_Premio { get; set; }
        public bool VisualizarMonedaLocal { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class TableroComercialCategoriaAsesorComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
    public class TableroComercialCategoriaAsesorDatosTableroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal MontoVenta { get; set; }
        public int? IdMonedaVenta { get; set; }
        public string? CodigoMonedaVenta { get; set; }
        public int? IdVisualizacionMonedaVenta { get; set; }
        public string? VisualizacionMonedaVenta { get; set; }
        public decimal MontoPremio { get; set; }
        public int? IdMonedaPremio { get; set; }
        public string? CodigoMonedaPremio { get; set; }
        public bool VisualizarMonedaLocal { get; set; }
    }
}