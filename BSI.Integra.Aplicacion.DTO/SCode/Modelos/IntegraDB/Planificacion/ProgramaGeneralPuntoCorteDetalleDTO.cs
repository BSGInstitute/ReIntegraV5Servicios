namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    public class ProgramaGeneralPuntoCorteDetalleDTO
    {
        public int Id { get; set; }
        public int? IdProgramaGeneralPuntoCorte { get; set; }
        public int? IdPuntoCorte { get; set; }
        public string Tipo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal ValorMinimo { get; set; }
        public decimal ValorMaximo { get; set; }
    }
}
