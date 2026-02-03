using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.Calidad.TranscriptionDTO;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial
{
    public class TranscriptionDTO
    {
        public class TranscriptionWebhookPayload
        {
            [JsonPropertyName("idLlamada")]
            public string? IdLlamada { get; set; }

            [JsonPropertyName("idActividadDetalle")]
            public string? IdActividadDetalle { get; set; }

            [JsonPropertyName("status")]
            public string? Status { get; set; }

            [JsonPropertyName("transcription")]
            public TranscriptionData? Transcription { get; set; }
        }
    }
}
