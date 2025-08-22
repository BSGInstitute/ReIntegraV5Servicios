namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class GmailCorreoDTO
    {
        public int Id { get; set; }
        public int? IdEtiqueta { get; set; }
        public int? IdGmailCliente { get; set; }
        public string? IdCorreoGmailFormat { get; set; }
        public string? Asunto { get; set; }
        public DateTime? Fecha { get; set; }
        public string? EmailBody { get; set; }
        public bool? Seen { get; set; }
        public string? Remitente { get; set; }
        public string? Destinatarios { get; set; }
        public int? IdPersonal { get; set; }
        public int? Filas { get; set; }
        public int? IdInteraccion { get; set; }
        public string? Cc { get; set; }
        public string? ResumenMensaje { get; set; }
        public string? Bcc { get; set; }
        public int? IdClasificacionPersona { get; set; } 
        public List<GmailCorreoArchivoAdjuntoDTO> ListaGmailCorreoArchivoAdjunto { get; set; }

    }
    public class GmailCorreoComboDTO
    {
        public int Id { get; set; }
        public string? Asunto { get; set; }
    }
    public class FiltroBandejaCorreoDTO
    {
        public int? IdAlumno { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int IdAsesor { get; set; }
        public string? Folder { get; set; }
        public string? TipoCorreos { get; set; }
        public GridFiltrosDTO? FiltroKendo { get; set; }
    }
    public class FiltroBandejaCorreoParaRepositorioDTO
    {
        public int IdPersonal { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public string Destinatarios { get; set; }
        public string Asunto { get; set; }
        public string Remitente { get; set; }
    }
    public class CorreoEnviadoPorPersonalDTO
    {
        public int Id { get; set; }
        public string Asunto { get; set; }
        public DateTime Fecha { get; set; }
        public string Remitente { get; set; }
        public bool Seen { get; set; }
        public string Destinatarios { get; set; }
        public int? TotalCorreos { get; set; }
        public bool? EnvioIndividualMandrill { get; set; }
    }
    public class BandejaCorreoEnviadoPorPersonalDTO
    {
        public IEnumerable<CorreoEnviadoPorPersonalDTO> ListaCorreos { get; set; } = new List<CorreoEnviadoPorPersonalDTO>();
        public int TotalEnviados { get; set; }
    }
    public class CorreosGmailDTO
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public string Email { get; set; }
        public string Nombres { get; set; }
    }
    public class CorreoArchivoAdjuntoDTO
    {
        public int Id { get; set; }
        public int IdCorreo { get; set; }
        public string? NombreArchivo { get; set; }
        public string? UrlArchivoRepositorio { get; set; }
    }
    public class GmailCorreoDTO2
    {
        // ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        public List<GmailCorreoArchivoAdjuntoDTO> ListaGmailCorreoArchivoAdjunto = new List<GmailCorreoArchivoAdjuntoDTO>();
    }
}
