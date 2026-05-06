using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing
{
    // ---------------------------------------------------------------------------
    // Historial de Oportunidades por Alumno — DTO de respuesta del SP
    // mkt.SP_OportunidadHistorialPorAlumno
    // Autor: develop-mvaldiviac | Fecha: 2026-05-05
    // ---------------------------------------------------------------------------

    /// <summary>
    /// Representa una oportunidad del historial de un alumno,
    /// retornada por el SP mkt.SP_OportunidadHistorialPorAlumno.
    /// </summary>
    public class HistorialOportunidadMasivoDTO
    {
        public int IdOportunidad { get; set; }
        public string FaseOportunidad { get; set; }  // = fase_cierre en IA
        public string FaseMaxima { get; set; }
        public string NombrePrograma { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public string Asesor { get; set; }
    }

    // ---------------------------------------------------------------------------
    // Calificacion Batch V2 — DTOs de request (perfil_lead + historial_oportunidades)
    // Autor: develop-mvaldiviac | Fecha: 2026-05-05
    // ---------------------------------------------------------------------------

    public class CalificacionBatchV2RequestDTO
    {
        [JsonProperty("tenant_id")]
        public string TenantId { get; set; }

        [JsonProperty("oportunidades")]
        public List<CalificacionLeadV2PayloadDTO> Oportunidades { get; set; }
    }

    public class CalificacionLeadV2PayloadDTO
    {
        [JsonProperty("identificador_lead")]
        public string IdentificadorLead { get; set; }

        [JsonProperty("agent_id")]
        public string AgentId { get; set; }

        [JsonProperty("origen")]
        public string Origen { get; set; }

        [JsonProperty("perfil_lead")]
        public CalificacionPerfilV2DTO PerfilLead { get; set; }

        [JsonProperty("historial_oportunidades")]
        public List<CalificacionHistorialV2ItemDTO> HistorialOportunidades { get; set; }

        [JsonProperty("mensajes")]
        public List<ExtraccionMensajeDTO> Mensajes { get; set; }
    }

    public class CalificacionPerfilV2DTO
    {
        [JsonProperty("area_formacion")]
        public string AreaFormacion { get; set; }

        [JsonProperty("cargo_actual")]
        public string CargoActual { get; set; }

        [JsonProperty("area_trabajo")]
        public string AreaTrabajo { get; set; }

        [JsonProperty("industria")]
        public string Industria { get; set; }
    }

    public class CalificacionHistorialV2ItemDTO
    {
        [JsonProperty("id_oportunidad")]
        public string IdOportunidad { get; set; }

        [JsonProperty("fase_maxima")]
        public string FaseMaxima { get; set; }

        [JsonProperty("fase_cierre")]
        public string FaseCierre { get; set; }
    }

    // ---------------------------------------------------------------------------
    // Extraccion Batch — DTOs de request para api-ia.bsginstitute.com
    // Autor: develop-mvaldiviac | Fecha: 2026-05-04
    // ---------------------------------------------------------------------------

    // ---------------------------------------------------------------------------
    // DTOs de respuesta compartidos entre Extraccion y Calificacion Batch
    // Representan el contrato de respuesta de /batches/{id}/status
    // ---------------------------------------------------------------------------

    /// <summary>
    /// Respuesta del endpoint GET /batches/{callId}/status del servicio de IA.
    /// Propiedades en snake_case decoradas con [JsonProperty] para que
    /// Newtonsoft.Json las mapee correctamente al deserializar.
    /// </summary>
    public class EstadoBatchResponseDTO
    {
        [JsonProperty("call_id")]
        public string CallId { get; set; }

        [JsonProperty("tenant_id")]
        public string TenantId { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("pending")]
        public int Pending { get; set; }

        [JsonProperty("processing")]
        public int Processing { get; set; }

        [JsonProperty("done")]
        public int Done { get; set; }

        [JsonProperty("errors")]
        public int Errors { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("expires_at")]
        public string ExpiresAt { get; set; }
    }

    /// <summary>
    /// Respuesta del endpoint GET /batches/{callId}/resultados del servicio de IA.
    /// Envuelve la lista de resultados individuales por lead.
    /// </summary>
    public class ResultadosBatchResponseDTO
    {
        [JsonProperty("call_id")]
        public string CallId { get; set; }

        [JsonProperty("tenant_id")]
        public string TenantId { get; set; }

        [JsonProperty("resultados")]
        public List<ResultadoLeadDTO> Resultados { get; set; }
    }

    /// <summary>
    /// Resultado individual de un lead dentro de un batch de extraccion.
    /// </summary>
    public class ResultadoLeadDTO
    {
        [JsonProperty("conv_id")]
        public string ConvId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("datos")]
        public object Datos { get; set; }
    }

    public class ExtraccionBatchRequestDTO
    {
        [JsonProperty("tenant_id")]
        public string TenantId { get; set; }

        [JsonProperty("conversations")]
        public List<ExtraccionLeadPayloadDTO> Conversations { get; set; }
    }

    public class ExtraccionLeadPayloadDTO
    {
        [JsonProperty("conv_id")]
        public string ConvId { get; set; }

        [JsonProperty("agent_id")]
        public string AgentId { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("pais")]
        public string Pais { get; set; }

        [JsonProperty("chat_datetime")]
        public string ChatDatetime { get; set; }

        [JsonProperty("messages")]
        public List<ExtraccionMensajeDTO> Messages { get; set; }
    }

    public class ExtraccionMensajeDTO
    {
        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }

    // ---------------------------------------------------------------------------
    // Calificacion Batch — DTOs de request para api-ia.bsginstitute.com
    // ---------------------------------------------------------------------------

    public class CalificacionLlamadaRequestDTO
    {
        [JsonProperty("tenant_id")]
        public string TenantId { get; set; }

        [JsonProperty("oportunidades")]
        public List<CalificacionLeadPayloadDTO> Oportunidades { get; set; }  // era Llamadas
    }

    public class CalificacionLeadPayloadDTO
    {
        [JsonProperty("identificador_lead")]
        public string IdentificadorLead { get; set; }

        [JsonProperty("agent_id")]
        public string AgentId { get; set; }

        [JsonProperty("origen")]
        public string Origen { get; set; }

        [JsonProperty("perfil")]
        public object Perfil { get; set; }

        [JsonProperty("historial")]
        public List<object> Historial { get; set; }

        [JsonProperty("mensajes")]
        public List<ExtraccionMensajeDTO> Mensajes { get; set; }  // reusar DTO de extraccion
    }
}
