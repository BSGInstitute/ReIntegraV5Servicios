namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MonedaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string NombreCorto { get; set; } = null!;
        public string NombrePlural { get; set; } = null!;
        public string Simbolo { get; set; } = null!;
        public string? Codigo { get; set; }
        public int IdPais { get; set; }
        public int DigitoFinanzas { get; set; }
        public bool? ValidaProcesoSeleccion { get; set; }
        public bool? VisualizarTableroComercial { get; set; }
        public bool? VisualizarFinanzas { get; set; }
        public decimal? PorcentajeMora { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class MonedaComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string NombreCorto { get; set; } = null!;
        public string NombrePlural { get; set; } = null!;
        public string Simbolo { get; set; } = null!;
        public int IdPais { get; set; }
    }
    public class MonedaNombrePluralSimboloDTO
    {
        public int Id { get; set; }
        public string NombrePlural { get; set; } = null!;
        public string Simbolo { get; set; } = null!;
    }
    public class MonedaCodigoCambioDTO
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public float? DolarAMoneda { get; set; }
        public float? MonedaADolar { get; set; }
    }
    public class MontoMonedaDTO
    {
        public string Cuota { get; set; }
        public string Moneda { get; set; }
    }
}
