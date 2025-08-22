namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SentinelSueldoPorIndustriaDataDinamicoDTO
    {
        public int Id { get; set; }
        public int? IdCargo { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdTamanioEmpresa { get; set; }
        public string? Nombre { get; set; }
        public int? Tipo { get; set; }
        public int? Valor { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class SentinelSueldoPorIndustriaDataDinamicoComboDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int? IdTamanioEmpresa { get; set; }
    }
}
