namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SentinelSueldoPorIndustriaDataTotalDTO
    {
        public int Id { get; set; }
        public int? IdCargo { get; set; }
        public int? IdIndustria { get; set; }
        public string? Nombre { get; set; }
        public int Tipo { get; set; }
        public int? Valor { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class SentinelSueldoPorIndustriaDataTotalComboDTO
    {
        public int Id { get; set; }
        public string? Industria { get; set; }
        public string? Cargo { get; set; }
        public string? Nombre { get; set; }
    }
}
