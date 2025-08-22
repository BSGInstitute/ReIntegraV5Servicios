namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    public class ProgramaGeneralPuntoCorteConfiguracionDTO
    {
        public int Id { get; set; }
        public int IdTipoCorte { get; set; }
        public string Tipo { get; set; } = null!;
        public int IdAreaCapacitacion { get; set; }
        public int IdSubAreaCapacitacion { get; set; }
        public int IdPgeneral { get; set; }
        public string? Color { get; set; }
        public string? Texto { get; set; }
    }
}
