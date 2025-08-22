namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProgramaGeneralCertificacionDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; } = null!;
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ProgramaGeneralCertificacionComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
    public class ProgramaGeneralCertificacionAgendaDTO
    {
        public int IdCertificacion { get; set; }
        public string? NombreCertificacion { get; set; }
        public int Respuesta { get; set; }
        public string? Completado { get; set; }
    }
    public class ProgramaGeneralCertificacionDetalleAgendaDTO
    {
        public int IdCertificacion { get; set; }
        public string? NombreCertificacion { get; set; }
        public int Respuesta { get; set; }
        public string? Completado { get; set; }
        public List<ProgramaGeneralCertificacionArgumentoComboDTO> Requisitos { get; set; } = new List<ProgramaGeneralCertificacionArgumentoComboDTO>();
    }
}
