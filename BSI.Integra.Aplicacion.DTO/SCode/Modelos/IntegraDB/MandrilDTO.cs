namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MandrilDTO
    {
        public int Id { get; set; }
        public int? IdAlumno { get; set; }
        public string? Evento { get; set; }
        public string? IdEvent { get; set; }
        public string? Ip { get; set; }
        public DateTime? Ts { get; set; }
        public string? Url { get; set; }
        public string? UserAgent { get; set; }
        public string? LocationCity { get; set; }
        public string? LocationCountry { get; set; }
        public string? LocationCountryShort { get; set; }
        public decimal? LocationLatitude { get; set; }
        public decimal? LocationLongitude { get; set; }
        public string? LocationPostalCode { get; set; }
        public string? LocationRegion { get; set; }
        public string? LocationTimezone { get; set; }
        public bool? UserAgentMobile { get; set; }
        public string? UserAgentOsCompany { get; set; }
        public string? UserAgentOsCompanyUrl { get; set; }
        public string? UserAgentOsFamily { get; set; }
        public string? UserAgentOsIcon { get; set; }
        public string? UserAgentOsName { get; set; }
        public string? UserAgentOsUrl { get; set; }
        public string? UserAgentType { get; set; }
        public string? UserAgentUaCompany { get; set; }
        public string? UserAgentUaCompanyUrl { get; set; }
        public string? UserAgentUaFamily { get; set; }
        public string? UserAgentUaIcon { get; set; }
        public string? UserAgentUaName { get; set; }
        public string? UserAgentUaUrl { get; set; }
        public string? UserAgentUaVersion { get; set; }
        public int? MessageBgToolsCode { get; set; }
        public string? MessageBounceDescription { get; set; }
        public string? MessageDiag { get; set; }
        public string? MessageEmail { get; set; }
        public string? MessageId { get; set; }
        public string? MessageSender { get; set; }
        public string? MessageState { get; set; }
        public string? MessageSubAccount { get; set; }
        public string? MessageSubject { get; set; }
        public string? MessageTags { get; set; }
        public string? MessageTemplate { get; set; }
        public DateTime? MessageTs { get; set; }
        public string? MessageVersion { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class CorreoInteraccionV2AgendaDTO
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Categoria { get; set; } = null!;
        public string? Asunto { get; set; }
        public string CorreoReceptor { get; set; } = null!;
        public string CorreoRemitente { get; set; } = null!;
        public int IdAlumno { get; set; }
        public int IdPersonal { get; set; }
        public string MessageId { get; set; } = null!;
        public string? UltimaInteraccion { get; set; }
    }
}
