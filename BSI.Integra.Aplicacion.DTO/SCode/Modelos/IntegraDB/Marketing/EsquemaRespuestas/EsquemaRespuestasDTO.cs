using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.EsquemaRespuestas
{
    public class UpsertAsistenteWhatsAppAsignacionDTO
    {
        public string NumeroWhatsApp    { get; set; }
        public int    EsquemaRespuesta  { get; set; }
        public bool   Estado            { get; set; }
    }

    public class AsistenteWhatsAppAsignacionNumeroDTO
    {
        [JsonProperty("Id")]
        public int    IdNumero             { get; set; }

        [JsonProperty("NumeroWhatsApp")]
        public string NumeroAsociado       { get; set; }

        [JsonProperty("NumeroIdentificador")]
        public string NumeroIdentificador  { get; set; }

        [JsonProperty("IdPais")]
        public int    IdPais               { get; set; }

        [JsonProperty("EsquemaRespuesta")]
        public int    EsquemaRespuesta     { get; set; }
    }

    public class ChatbotEsquemaAsignacionFaseDTO
    {
        public int IdChatbotEsquemaAsignacionFase { get; set; }
        public string NombreFase { get; set; }
    }

    public class ChatbotEsquemaAsignacionMensajeExactoDTO
    {
        public int IdChatbotEsquemaAsignacionMensajeExacto { get; set; }
        public string NombreMensajeExacto { get; set; }
    }

    public class ChatbotEsquemaAsignacionPerfilDTO
    {
        public int IdChatbotEsquemaAsignacionPerfil { get; set; }
        public string NombrePerfil { get; set; }
    }

    // ─── Response DTOs de listado ─────────────────────────────────────────────

    public class EsquemaListadoCompletoDTO
    {
        public int IdChatbotEsquema { get; set; }
        public string Nombre { get; set; }
        public string Restricciones { get; set; }
        public int TotalLecturas => LecturasMensajes?.Count ?? 0;
        public int TotalInterpretaciones => InterpretarInformacion?.Count ?? 0;
        public List<LecturaMensajeDetalleDTO> LecturasMensajes { get; set; } = new();
        public List<InterpretarInformacionDetalleDTO> InterpretarInformacion { get; set; } = new();
        public List<EsquemaRespuestaDetalleDTO> EsquemasRespuesta { get; set; } = new();
    }

    // ─── Response DTOs de detalle por Id ──────────────────────────────────────

    public class EsquemaDetalleResponseDTO
    {
        public int IdChatbotEsquema { get; set; }
        public string Nombre { get; set; }
        public string Restricciones { get; set; }
        public List<LecturaMensajeDetalleDTO> LecturasMensajes { get; set; } = new();
        public List<InterpretarInformacionDetalleDTO> InterpretarInformacion { get; set; } = new();
        public List<EsquemaRespuestaDetalleDTO> EsquemasRespuesta { get; set; } = new();
    }

    public class LecturaMensajeDetalleDTO
    {
        public string Clasificacion { get; set; }
        public string PromptLectura { get; set; }
        public List<string> MensajesExactos { get; set; } = new();
    }

    public class InterpretarInformacionDetalleDTO
    {
        public string Nombre { get; set; }
        public List<string> Clasificaciones { get; set; } = new();
        public List<SubcategoriaDetalleDTO> Subcategorias { get; set; } = new();
    }

    public class SubcategoriaDetalleDTO
    {
        public string Nombre { get; set; }
        public List<string> FasMaximaValores { get; set; } = new();
        public List<string> PerfilValores { get; set; } = new();
    }

    public class EsquemaRespuestaDetalleDTO
    {
        public string Clasificacion { get; set; }
        public string? Subcategoria { get; set; }
        public string ParametrosRespuesta { get; set; }
    }

    // ─── Request DTOs para insertar un esquema completo ───────────────────────

    public class CrearEsquemaRequestDTO
    {
        public string Nombre { get; set; }
        public string Restricciones { get; set; }
        public List<string> NuevosMensajesExactos { get; set; } = new();
        public List<LecturaMensajeRequestDTO> LecturasMensajes { get; set; } = new();
        public List<InterpretarInformacionRequestDTO> InterpretarInformacion { get; set; } = new();
        public List<EsquemaRespuestaItemRequestDTO> EsquemasRespuesta { get; set; } = new();
    }

    public class ActualizarEsquemaRequestDTO
    {
        public int IdChatbotEsquema { get; set; }
        public string Nombre { get; set; }
        public string Restricciones { get; set; }
        public List<string> NuevosMensajesExactos { get; set; } = new();
        public List<LecturaMensajeRequestDTO> LecturasMensajes { get; set; } = new();
        public List<InterpretarInformacionRequestDTO> InterpretarInformacion { get; set; } = new();
        public List<EsquemaRespuestaItemRequestDTO> EsquemasRespuesta { get; set; } = new();
    }

    public class LecturaMensajeRequestDTO
    {
        public string Clasificacion { get; set; }
        public string PromptLectura { get; set; }
        public List<string> MensajesExactos { get; set; } = new();
    }

    public class InterpretarInformacionRequestDTO
    {
        public string Nombre { get; set; }
        public List<string> Clasificaciones { get; set; } = new();
        public List<ChatbotSubcategoriaRequestDTO> Subcategorias { get; set; } = new();
    }

    public class ChatbotSubcategoriaRequestDTO
    {
        public string Nombre { get; set; }
        public List<string> FasMaximaValores { get; set; } = new();
        public List<string> PerfilValores { get; set; } = new();
    }

    public class EsquemaRespuestaItemRequestDTO
    {
        public string Clasificacion { get; set; }
        public string Subcategoria { get; set; }
        public string ParametrosRespuesta { get; set; }
    }
}
