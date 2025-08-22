namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ConfiguracionFijaDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string NombreTabla { get; set; } = null!;
        public int IdTabla { get; set; }
        public string NombreColumna { get; set; } = null!;
        public string TipoDato { get; set; } = null!;
        public string Valor { get; set; } = null!;
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ConfiguracionFijaComboDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
    }
}
