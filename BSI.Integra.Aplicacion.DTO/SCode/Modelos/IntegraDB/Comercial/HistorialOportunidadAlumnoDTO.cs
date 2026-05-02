using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial
{
    /// Autor: Jose Vega
    /// Fecha: 22/04/2026
    /// Version: 1.0
    /// <summary>
    /// Respuesta del endpoint de historial de oportunidades por alumno con
    /// contadores de interacciones por canal.
    /// </summary>
    public class HistorialOportunidadAlumnoDTO
    {
        [JsonPropertyName("idAlumno")]
        public int IdAlumno { get; set; }

        [JsonPropertyName("historialOportunidades")]
        public List<OportunidadHistorialV2DTO> HistorialOportunidades { get; set; } = new List<OportunidadHistorialV2DTO>();
    }

    public class OportunidadHistorialV2DTO
    {
        [JsonPropertyName("idOportunidad")]
        public int IdOportunidad { get; set; }

        [JsonPropertyName("fechaCreacion")]
        public string FechaCreacion { get; set; } = string.Empty;

        [JsonPropertyName("faseMaxima")]
        public string FaseMaxima { get; set; } = string.Empty;

        [JsonPropertyName("faseCierre")]
        public string FaseCierre { get; set; } = string.Empty;

        [JsonPropertyName("interacciones")]
        public InteraccionesOportunidadDTO Interacciones { get; set; } = new InteraccionesOportunidadDTO();
    }

    public class InteraccionesOportunidadDTO
    {
        [JsonPropertyName("llamadas")]
        public LlamadasInteraccionDTO Llamadas { get; set; } = new LlamadasInteraccionDTO();

        [JsonPropertyName("whatsapp")]
        public WhatsappInteraccionDTO Whatsapp { get; set; } = new WhatsappInteraccionDTO();

        [JsonPropertyName("correo")]
        public CorreoInteraccionDTO Correo { get; set; } = new CorreoInteraccionDTO();

        [JsonPropertyName("portal_web")]
        public PortalWebInteraccionDTO PortalWeb { get; set; } = new PortalWebInteraccionDTO();
    }

    public class LlamadasInteraccionDTO
    {
        [JsonPropertyName("ejecutadas")]
        public int Ejecutadas { get; set; }

        [JsonPropertyName("no_ejecutadas")]
        public int NoEjecutadas { get; set; }

        [JsonPropertyName("manual")]
        public int Manual { get; set; }
    }

    public class WhatsappInteraccionDTO
    {
        [JsonPropertyName("mensajes_usuario")]
        public int MensajesUsuario { get; set; }
    }

    public class CorreoInteraccionDTO
    {
        [JsonPropertyName("correos_usuario")]
        public int CorreosUsuario { get; set; }
    }

    public class PortalWebInteraccionDTO
    {
        [JsonPropertyName("mensajes_usuario")]
        public int MensajesUsuario { get; set; }
    }

    public class HistorialOportunidadPlanoDTO
    {
        public int IdOportunidad { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public string? FaseMaxima { get; set; }
        public string? FaseCierre { get; set; }
        public int LlamadasEjecutadas { get; set; }
        public int LlamadasNoEjecutadas { get; set; }
        public int LlamadasManual { get; set; }
        public int WhatsappMensajesUsuario { get; set; }
        public int CorreoCorreosUsuario { get; set; }
        public int PortalWebMensajesUsuario { get; set; }
    }
}
