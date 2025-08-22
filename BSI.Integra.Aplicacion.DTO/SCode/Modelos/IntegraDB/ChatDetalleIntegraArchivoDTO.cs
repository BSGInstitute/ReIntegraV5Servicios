namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ChatDetalleIntegraArchivoDTO
    {
        public int Id { get; set; }
        public string? NombreArchivo { get; set; }
        public string? RutaArchivo { get; set; }
        public string? MimeType { get; set; }
        public bool? EsImagen { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ChatDetalleIntegraArchivoComboDTO
    {
        public int Id { get; set; }
        public string? NombreArchivo { get; set; }
    }
}
