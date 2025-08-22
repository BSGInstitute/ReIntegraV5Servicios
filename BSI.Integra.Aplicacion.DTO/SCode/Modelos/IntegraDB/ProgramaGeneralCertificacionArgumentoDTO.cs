namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProgramaGeneralCertificacionArgumentoDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralCertificacion { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPgeneral { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ProgramaGeneralCertificacionArgumentoComboDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralCertificacion { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
