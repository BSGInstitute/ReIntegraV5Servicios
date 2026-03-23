using Newtonsoft.Json;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.Configuracion
{
    // ─── Request al endpoint interno ──────────────────────────────────────────

    public class SincronizarEsquemaRequestDTO
    {
        public int IdChatbotEsquema { get; set; }
    }

    // ─── JSON que se envía a la API externa (PATCH /api/configuracion/interaccion/) ─

    public class InteraccionPatchDTO
    {
        [JsonProperty("esquemas")]
        public List<EsquemaPatchDTO> Esquemas { get; set; } = new();
    }

    public class EsquemaPatchDTO
    {
        [JsonProperty("id_esquema")]
        public int IdEsquema { get; set; }

        [JsonProperty("nombre_esquema")]
        public string NombreEsquema { get; set; }

        [JsonProperty("restricciones")]
        public string Restricciones { get; set; }

        [JsonProperty("mensajes")]
        public List<MensajePatchDTO> Mensajes { get; set; } = new();

        [JsonProperty("perfiles")]
        public List<PerfilPatchDTO> Perfiles { get; set; } = new();

        [JsonProperty("respuestas")]
        public List<RespuestaPatchDTO> Respuestas { get; set; } = new();
    }

    public class MensajePatchDTO
    {
        [JsonProperty("id_etiqueta")]
        public int IdEtiqueta { get; set; }

        [JsonProperty("nombre_etiqueta")]
        public string NombreEtiqueta { get; set; }

        [JsonProperty("usa_prompt")]
        public bool UsaPrompt { get; set; } = true;

        [JsonProperty("usa_mensaje_exacto")]
        public bool UsaMensajeExacto { get; set; } = true;

        [JsonProperty("interpretacion_prompt")]
        public string InterpretacionPrompt { get; set; }

        [JsonProperty("mensaje_exacto")]
        public List<MensajeExactoPatchDTO> MensajeExacto { get; set; } = new();
    }

    public class MensajeExactoPatchDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("texto")]
        public string Texto { get; set; }

        [JsonProperty("tipo")]
        public string Tipo { get; set; } = "texto";

        [JsonProperty("ubicacion")]
        public string Ubicacion { get; set; } = "todo";
    }

    public class PerfilPatchDTO
    {
        [JsonProperty("id_perfil")]
        public int IdPerfil { get; set; }

        [JsonProperty("nombre_perfil")]
        public string NombrePerfil { get; set; }

        [JsonProperty("fases")]
        public List<string> Fases { get; set; } = new();
    }

    public class RespuestaPatchDTO
    {
        [JsonProperty("id_respuesta")]
        public int IdRespuesta { get; set; }

        [JsonProperty("id_etiqueta")]
        public int IdEtiqueta { get; set; }

        [JsonProperty("instrucciones_etiqueta")]
        public string InstruccionesEtiqueta { get; set; }
    }

    // ─── Respuesta de la API externa ──────────────────────────────────────────

    public class ConfiguracionApiResponseDTO
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("archivo")]
        public string Archivo { get; set; }

        [JsonProperty("campos_aplicados")]
        public List<string> CamposAplicados { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }
    }
}
