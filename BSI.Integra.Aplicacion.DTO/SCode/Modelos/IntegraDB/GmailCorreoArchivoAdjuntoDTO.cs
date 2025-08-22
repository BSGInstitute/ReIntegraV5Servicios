namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class GmailCorreoArchivoAdjuntoDTO
    {
        public int Id { get; set; }
        public int IdGmailCorreo { get; set; }
        public string? Nombre { get; set; }
        public string? UrlArchivoRepositorio { get; set; }
        public int? IdMigracion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
    }
}
