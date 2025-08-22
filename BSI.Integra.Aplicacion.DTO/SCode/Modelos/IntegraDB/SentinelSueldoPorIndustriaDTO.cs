namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SentinelSueldoPorIndustriaDTO
    {
        public int? IdCargo { get; set; }
        public int? IdIndustria { get; set; }
        public int? Tipo { get; set; }
    }
    public class SentinelSueldoPorIndustriaComboDTO
    {
        public int Id { get; set; }
        public string? Industria { get; set; }
        public string? Cargo { get; set; }
    }
}
