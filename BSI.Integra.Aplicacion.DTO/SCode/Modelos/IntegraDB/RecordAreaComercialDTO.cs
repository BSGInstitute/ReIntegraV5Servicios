namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class RecordAreaComercialDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Monto { get; set; }
        public int IdMonedaRecord { get; set; }
        public int IdTableroComercialUnidad { get; set; }
        public decimal Bono { get; set; }
        public int IdMonedaBono { get; set; }
        public bool VisualizarMonedaLocal { get; set; }
        public bool EsVigente { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class RecordAreaComercialComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
    public class RecordAreaComercialCompuestoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Monto { get; set; }
        public int? IdMonedaRecord { get; set; }
        public string? CodigoMonedaRecord { get; set; }
        public int? IdTableroComercialUnidad { get; set; }
        public string? TableroComercialUnidad { get; set; }
        public decimal Bono { get; set; }
        public int? IdMonedaBono { get; set; }
        public string? CodigoMonedaBono { get; set; }
        public bool VisualizarMonedaLocal { get; set; }
        public bool EsVigente { get; set; }
        public string Vigente { get; set; } = null!;
    }
    public class RecordAreaComercialCrudDTO
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public decimal? Monto { get; set; }
        public int? IdMonedaRecord { get; set; }
        public int? IdTableroComercialUnidad { get; set; }
        public decimal? Bono { get; set; }
        public int? IdMonedaBono { get; set; }
        public bool? VisualizarMonedaLocal { get; set; }
        public bool? EsVigente { get; set; }
        public string Usuario { get; set; }
    }
}
