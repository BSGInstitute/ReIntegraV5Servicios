using Google.Api.Ads.AdWords.v201809;
using System.Web;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class WhatsAppConfiguracionApiDTO
    {
        public int Id { get; set; }
        public string? Numero { get; set; }
        public string? VName { get; set; }
        public int? IdPais { get; set; }
        public string? Bearer { get; set; }
        public string? NumeroIndentificador { get; set; }
        public string? VersionApi { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public bool? EsMigracion { get; set; }
        public bool? Estado { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public int? IdMigracion { get; set; }
        public string? NumeroWhatsApp { get; set; }
        public string? CuentaIdentificadorWhatsApp { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public string? CodigoArea { get; set; }
        public int? IdPersonal_Asignado { get; set; }
    }

    public class WhatsAppConfiguracionApiInsertDTO
    {
        public int Id { get; set; }
        public string? Numero { get; set; }
        public string? VName { get; set; }
        public int? IdPais { get; set; }
        public string? Bearer { get; set; }
        public string? NumeroIndentificador { get; set; }
        public string? VersionApi { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public string? NumeroWhatsApp { get; set; }
        public string? CuentaIdentificadorWhatsApp { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public string? CodigoArea { get; set; }
        public int? IdPersonal_Asignado { get; set; }
    }

    public class WhatsAppConfiguracionApiEntradaDTO
    {
        public int? Id { get; set; }
        public string Numero { get; set; }
        public string VName { get; set; }
        public int IdPais { get; set; }
        public string Bearer { get; set; }
        public string NumeroIndentificador { get; set; }
        public string VersionApi { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string NumeroWhatsApp { get; set; }
        public string CuentaIdentificadorWhatsApp { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public string CodigoArea { get; set; }
        public int IdPersonal_Asignado { get; set; }
    }

    public class WhatsAppConfiguracionApiListaGrillaDTO
    {
        public int? IdWhatsAppConfiguracionApi { get; set;}
        public string? Numero { get; set; }
        public string? VName { get; set; }
        public int IdPais { get; set; }
        public string? Pais { get; set; }
        public string? Bearer { get; set; }
        public string? NumeroIndentificador { get; set; }
        public string? VersionApi { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public string? NumeroWhatsApp { get; set; }
        public string? CuentaIdentificadorWhatsApp { get; set; }
        public int? IdPersonal { get; set; }
        public string? Personal { get; set; }
        public int? IdArea { get; set; }
        public string? Area { get; set; }
    }


    public class WhatsAppConfiguracionApiNumeroIdentificadorDto
    {
        public int IdWhatsAppConfiguracionApi { get; set; }
        public string CuentaIdentificadorWhatsApp { get; set; }
        public string NumeroIndentificador { get; set; }
        public int IdPais { get; set; }
        public int IdPersonalAsignado { get; set; }
    }
}
