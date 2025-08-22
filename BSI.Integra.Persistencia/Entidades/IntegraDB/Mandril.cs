using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Mandril : BaseIntegraEntity
    {
        public int? IdAlumno { get; set; }
        [StringLength(100)]
        public string? Evento { get; set; }
        [StringLength(100)]
        public string? IdEvent { get; set; }
        [StringLength(100)]
        public string? Ip { get; set; }
        public DateTime? Ts { get; set; }
        [StringLength(400)]
        public string? Url { get; set; }
        [StringLength(400)]
        public string? UserAgent { get; set; }
        [StringLength(200)]
        public string? LocationCity { get; set; }
        [StringLength(200)]
        public string? LocationCountry { get; set; }
        [StringLength(100)]
        public string? LocationCountryShort { get; set; }
        public decimal? LocationLatitude { get; set; }
        public decimal? LocationLongitude { get; set; }
        [StringLength(100)]
        public string? LocationPostalCode { get; set; }
        [StringLength(100)]
        public string? LocationRegion { get; set; }
        [StringLength(100)]
        public string? LocationTimezone { get; set; }
        public bool? UserAgentMobile { get; set; }
        [StringLength(200)]
        public string? UserAgentOsCompany { get; set; }
        [StringLength(400)]
        public string? UserAgentOsCompanyUrl { get; set; }
        [StringLength(200)]
        public string? UserAgentOsFamily { get; set; }
        [StringLength(400)]
        public string? UserAgentOsIcon { get; set; }
        [StringLength(200)]
        public string? UserAgentOsName { get; set; }
        [StringLength(400)]
        public string? UserAgentOsUrl { get; set; }
        [StringLength(200)]
        public string? UserAgentType { get; set; }
        [StringLength(200)]
        public string? UserAgentUaCompany { get; set; }
        [StringLength(200)]
        public string? UserAgentUaCompanyUrl { get; set; }
        [StringLength(200)]
        public string? UserAgentUaFamily { get; set; }
        [StringLength(300)]
        public string? UserAgentUaIcon { get; set; }
        [StringLength(200)]
        public string? UserAgentUaName { get; set; }
        [StringLength(300)]
        public string? UserAgentUaUrl { get; set; }
        [StringLength(200)]
        public string? UserAgentUaVersion { get; set; }
        public int? MessageBgToolsCode { get; set; }
        [StringLength(300)]
        public string? MessageBounceDescription { get; set; }
        [StringLength(400)]
        public string? MessageDiag { get; set; }
        [StringLength(200)]
        public string? MessageEmail { get; set; }
        [StringLength(200)]
        public string? MessageId { get; set; }
        [StringLength(200)]
        public string? MessageSender { get; set; }
        [StringLength(200)]
        public string? MessageState { get; set; }
        [StringLength(200)]
        public string? MessageSubAccount { get; set; }
        [StringLength(300)]
        public string? MessageSubject { get; set; }
        [StringLength(1000)]
        public string? MessageTags { get; set; }
        [StringLength(1000)]
        public string? MessageTemplate { get; set; }
        public DateTime? MessageTs { get; set; }
        [StringLength(200)]
        public string? MessageVersion { get; set; }
        public int? IdTipoInteraccion { get; set; }
    }
}
