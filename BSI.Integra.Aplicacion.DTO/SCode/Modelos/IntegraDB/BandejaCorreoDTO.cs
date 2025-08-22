namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class BandejaCorreoDTO
    {
        public List<CorreoDTO> ListaCorreos { get; set; } = new List<CorreoDTO>();
        public int TotalEnviados { get; set; }
        public BandejaCorreoDTO()
        {
            ListaCorreos = new List<CorreoDTO>();
        }
    }
    public class CorreoDTO
    {
        public int Id { get; set; }
        public string Asunto { get; set; }
        public string EmailBody { get; set; }
        public DateTime Fecha { get; set; }
        public string Remitente { get; set; }
        public string Destinatarios { get; set; }
        public string From { get; set; }
        public bool Seen { get; set; }
        public int? TotalCorreos { get; set; }
        public int? IdPersonal { get; set; }
        public bool? EnvioMasivoMandrill { get; set; }
        public bool? EnvioIndividualMandrill { get; set; }
        public string ConCopia { get; set; }
        public int? IdAlumno { get; set; }
        public string MessageId { get; set; }
        public string Estado { get; set; }
        public string Categoria { get; set; }
        public string Tipo { get; set; }
    }
    public class ObtenerBandejaEntradaArgumentosDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Folder { get; set; }
        public GridFiltrosDTO? FiltroKendo { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
    }
    public class ListaCorreosGrupoDTO
    {
        public string ListaCorreos { get; set; }
        public int TotalCorreos { get; set; }
        public bool Errores { get; set; }
    }
}
