namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TableroComercialUnidadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int Valor { get; set; }
        public string? Simbolo { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class TableroComercialUnidadComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
    public class TableroComercialUnidadSinAuditoriaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int Valor { get; set; }
        public string? Simbolo { get; set; }
    }
}
