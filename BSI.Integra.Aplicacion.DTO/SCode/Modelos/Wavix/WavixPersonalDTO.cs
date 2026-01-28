using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.Wavix
{

    public class WavixPersonalDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int IdWavixCredencial { get; set; }
        public string IdSipTrunk { get; set; }
        public string UrlServer { get; set; }
        public int versionWavix { get; set; }

    }
    public class NumeroAsesorWavixDTO
    {
        public string IdPersonal { get; set; }
        public string NombreAsesor { get; set; }
        public string IdSipTrunk { get; set; }
        public string UrlServer { get; set; }
        public int IdPais { get; set; }
        public string Numero { get; set; }
        public bool Predeterminado { get; set; }
    }


    public class EstadoLlamadaDTO
    {
        public string uuid { get; set; }
        public int idOportunidad { get; set; }
        public int idActividadDetalle { get; set; }
        public string disposition { get; set; }
        public string answered_by { get; set; }

    }

    public class SipTrunkDTO
    {
        public string id { get; set; }
        public string access_token { get; set; }
        public string urlServer { get; set; }
        public string label { get; set; }
        public string name { get; set; }
        public string auth_method { get; set; }
        public string callerid { get; set; }
        public HostRequestDTO host_request { get; set; }
        public bool encrypted_media { get; set; }
        public bool passthrough { get; set; }
        public bool multiple_numbers { get; set; }
        public string status { get; set; }
        public decimal charge { get; set; }
        public int talk_time { get; set; }
        public bool machine_detection_enabled { get; set; }
        public bool call_recording_enabled { get; set; }
        public bool transcription_enabled { get; set; }
        public int transcription_threshold { get; set; }
    }

    public class HostRequestDTO
    {
        public string host { get; set; }
        public string status { get; set; }
    }

    public class PaginationDTO
    {
        public int current_page { get; set; }
        public int total { get; set; }
        public int per_page { get; set; }
        public int total_pages { get; set; }
    }

    public class SipTrunkListResponseDTO
    {
        public List<SipTrunkDTO> sip_trunks { get; set; }
        public PaginationDTO pagination { get; set; }
    }

    public class AllowedIpDTO
    {
        public int id { get; set; }
        public string ip { get; set; }
    }

    public class SipTrunkConfigDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        public string callerid { get; set; }
        public string label { get; set; }
        public string created_at { get; set; }
        public bool ip_restrict { get; set; }
        public bool channels_restrict { get; set; }
        public int? max_channels { get; set; }
        public bool cost_limit { get; set; }
        public string max_call_cost { get; set; }
        public bool? call_restrict { get; set; }
        public int? call_limit { get; set; }
        public bool didinfo_enabled { get; set; }
        public bool call_recording_enabled { get; set; }
        public bool machine_detection_enabled { get; set; }
        public bool transcription_enabled { get; set; }
        public bool rewrite_enabled { get; set; }
        public bool encrypted_media { get; set; }
        public bool multiple_numbers { get; set; }
        public List<AllowedIpDTO> allowed_ips { get; set; }
        public string host { get; set; }
        public string access_token { get; set; }
        public int transcription_threshold { get; set; }
    }

    public class GenerarTokenWidgetRequestDTO
    {
        public string sip_trunk { get; set; }
        public object payload { get; set; }
        public int? ttl { get; set; }
    }

    public class GenerarTokenWidgetResponseDTO
    {
        public string token { get; set; }
        public string uuid { get; set; }
        public string sip_trunk { get; set; }
        public object payload { get; set; }
        public int? ttl { get; set; }
    }

    public class ConfiguracionCompletaWavixDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        public string urlServer { get; set; }
        public string callerid { get; set; }
        public string token { get; set; }
        public string uuid { get; set; }
        public int? ttl { get; set; }
        public string accessToken { get; set; }
        public int versionWavix { get; set; }
    }

    public class TokenActivo { 
        public string tokenActivo { get; set; } 
    }

    public class TokenVigenteDTO {
        public int id { get; set; }
        public int idPersonalWavix { get; set; }
        public string tokenUuid { get; set; }
        public string token { get; set; }
    }

    /// <summary>
    /// DTO para listar tokens activos
    /// </summary>
    public class TokenActivoListDTO
    {
        public int Id { get; set; }
        public int IdPersonalWavix { get; set; }
        public string TokenUuid { get; set; }
        public string Token { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public bool EstaActivo { get; set; }
    }

    /// <summary>
    /// DTO para actualizar el payload de un token
    /// </summary>
    public class ActualizarTokenPayloadRequestDTO
    {
        public object Payload { get; set; }
    }

    /// <summary>
    /// DTO de respuesta para operaciones de token
    /// </summary>
    public class TokenOperacionResponseDTO
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public string TokenUuid { get; set; }
    }
}
