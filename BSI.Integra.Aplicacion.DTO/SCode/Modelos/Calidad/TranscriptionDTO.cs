using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.Calidad
{
    public class TranscriptionDTO
    {
        public class TranscriptionWebhookPayloadDTO
        {
            [JsonPropertyName("idLlamada")]
            public string? IdLlamada { get; set; }

            [JsonPropertyName("idActividadDetalle")]
            public string? IdActividadDetalle { get; set; }
            [JsonPropertyName("idPersonal")]
            public int? IdPersonal { get; set; }
            [JsonPropertyName("userName")]
            public string? UserName { get; set; }
            [JsonPropertyName("contacto")]
            public string? contacto { get; set; }

            [JsonPropertyName("status")]
            public string? Status { get; set; }

            [JsonPropertyName("transcription")]
            public TranscriptionData? Transcription { get; set; }
        }

        public class TranscriptionData
        {
            [JsonPropertyName("source")]
            public string? Source { get; set; }

            [JsonPropertyName("timestamp")]
            public DateTime Timestamp { get; set; }

            [JsonPropertyName("durationInTicks")]
            public long DurationInTicks { get; set; }

            [JsonPropertyName("durationMilliseconds")]
            public int DurationMilliseconds { get; set; }

            [JsonPropertyName("duration")]
            public string? Duration { get; set; }

            [JsonPropertyName("summary")]
            public string? Summary { get; set; }

            [JsonPropertyName("ocurrencia_consistente")]
            public string? OcurrenciaConsistente { get; set; }

            [JsonPropertyName("comentarioOcurrencia")]
            public string? ComentarioConsistenciaOcurrencia { get; set; }

            [JsonPropertyName("cambioFase_consistente")]
            public string? CambioFaseConsistente { get; set; }
            [JsonPropertyName("comentarioCambioFase")]
            public string? ComentarioConsistenciaCambioFase { get; set; }

            [JsonPropertyName("recomendaciones")]
            public List<string>? Recomendaciones { get; set; }

            [JsonPropertyName("combinedRecognizedPhrases")]
            public List<CombinedRecognizedPhrase>? CombinedRecognizedPhrases { get; set; }

            [JsonPropertyName("recognizedPhrases")]
            public List<RecognizedPhrase>? RecognizedPhrases { get; set; }
        }

        public class CombinedRecognizedPhrase
        {
            [JsonPropertyName("channel")]
            public int Channel { get; set; }

            [JsonPropertyName("lexical")]
            public string? Lexical { get; set; }

            [JsonPropertyName("itn")]
            public string? ITN { get; set; }

            [JsonPropertyName("maskedITN")]
            public string? MaskedITN { get; set; }

            [JsonPropertyName("display")]
            public string? Display { get; set; }
        }

        public class RecognizedPhrase
        {
            [JsonPropertyName("recognitionStatus")]
            public string? RecognitionStatus { get; set; }

            [JsonPropertyName("channel")]
            public int Channel { get; set; }

            [JsonPropertyName("speaker")]
            public string? Speaker { get; set; }

            [JsonPropertyName("offset")]
            public string? Offset { get; set; }

            [JsonPropertyName("duration")]
            public string? Duration { get; set; }

            [JsonPropertyName("offsetInTicks")]
            public double OffsetInTicks { get; set; }

            [JsonPropertyName("durationInTicks")]
            public double DurationInTicks { get; set; }

            [JsonPropertyName("durationMilliseconds")]
            public int DurationMilliseconds { get; set; }

            [JsonPropertyName("offsetMilliseconds")]
            public int OffsetMilliseconds { get; set; }

            [JsonPropertyName("nBest")]
            public List<NBestOption>? NBest { get; set; }
        }

        public class NBestOption
        {
            [JsonPropertyName("confidence")]
            public double Confidence { get; set; }

            [JsonPropertyName("lexical")]
            public string? Lexical { get; set; }

            [JsonPropertyName("itn")]
            public string? ITN { get; set; }

            [JsonPropertyName("maskedITN")]
            public string? MaskedITN { get; set; }

            [JsonPropertyName("display")]
            public string? Display { get; set; }

            [JsonPropertyName("sentiment")]
            public string? Sentiment { get; set; }
        }


        public class TranscripcionCompletaResponseDTO
        {
            public string IdLlamada { get; set; }
            public string? IdActividadDetalle { get; set; }
            public string Status { get; set; }
            public TranscriptionDto Transcription { get; set; }
        }
        public class TranscripcionCompletaResponseDisplayDTO
        {
            public string IdLlamada { get; set; }
            public string? IdActividadDetalle { get; set; }
            public string Status { get; set; }
            public TranscriptionDisplayDto Transcription { get; set; }
        }
        public class TranscriptionDto
        {
            public string Source { get; set; }
            public DateTime Timestamp { get; set; }
            public long DurationInTicks { get; set; }
            public int DurationMilliseconds { get; set; }
            public string Duration { get; set; }
            public string Summary { get; set; }
            public string Ocurrencia_Consistente { get; set; } // "si" o "no"
            public string ComentarioConsistenciaOcurrencia { get; set; }
            public string? CambioFaseConsistente { get; set; }
            public string? ComentarioConsistenciaCambioFase { get; set; }

            public List<CombinedRecognizedPhraseDto> CombinedRecognizedPhrases { get; set; }
            public List<RecognizedPhraseDto> RecognizedPhrases { get; set; }
            public List<string> Recomendaciones { get; set; }
        }
        public class TranscriptionDisplayDto
        {
            public string Source { get; set; }
            public DateTime Timestamp { get; set; }
            public long DurationInTicks { get; set; }
            public int DurationMilliseconds { get; set; }
            public string Duration { get; set; }
            public string Summary { get; set; }
            public string Ocurrencia_Consistente { get; set; } // "si" o "no"
            public string ComentarioConsistenciaOcurrencia { get; set; }
            public string? CambioFaseConsistente { get; set; }
            public string? ComentarioConsistenciaCambioFase { get; set; }

            public List<CombinedRecognizedPhraseDisplayDto> CombinedRecognizedPhrases { get; set; }
            public List<RecognizedPhraseDisplayDto> RecognizedPhrases { get; set; }
            public List<string> Recomendaciones { get; set; }
        }
        public class CombinedRecognizedPhraseDto
        {
            public int? Channel { get; set; }
            public string Lexical { get; set; }
            public string Itn { get; set; }
            public string MaskedITN { get; set; }
            public string Display { get; set; }
        }
        public class CombinedRecognizedPhraseDisplayDto
        {
            public int? Channel { get; set; }
            public string Display { get; set; }
        }
        public class RecognizedPhraseDto
        {
            public string RecognitionStatus { get; set; }
            public int? Channel { get; set; }
            public string Speaker { get; set; }
            public string Offset { get; set; }
            public string Duration { get; set; }
            public long? OffsetInTicks { get; set; }
            public long? DurationInTicks { get; set; }
            public int? DurationMilliseconds { get; set; }
            public int? OffsetMilliseconds { get; set; }
            public List<NBestDto> NBest { get; set; }
        }
        public class RecognizedPhraseDisplayDto
        {
            public string RecognitionStatus { get; set; }
            public int? Channel { get; set; }
            public string Speaker { get; set; }
            public string Offset { get; set; }
            public string Duration { get; set; }
            public long? OffsetInTicks { get; set; }
            public long? DurationInTicks { get; set; }
            public int? DurationMilliseconds { get; set; }
            public int? OffsetMilliseconds { get; set; }
            public List<NBestDisplayDto> NBest { get; set; }
        }
        public class NBestDto
        {
            public decimal? Confidence { get; set; }
            public string Lexical { get; set; }
            public string Itn { get; set; }
            public string MaskedITN { get; set; }
            public string Display { get; set; }
            public string Sentiment { get; set; }
        }
        public class NBestDisplayDto
        {
            public decimal? Confidence { get; set; }
            public string Display { get; set; }
            public string Sentiment { get; set; }
        }

        public class TranscripcionDetalleDTO
        {
            public int TranscripcionId { get; set; }
            public int IdLlamadaWebphoneCruceCentralTresCx { get; set; }
            public string Source { get; set; }
            public DateTime Timestamp { get; set; }
            public long? DurationInTicks { get; set; }
            public int? DurationMilliseconds { get; set; }
            public string Duration { get; set; }
            public string Summary { get; set; }
            public bool? OcurrenciaConsistente { get; set; }
            public string? ComentarioConsistenciaOcurrencia { get; set; }
            public bool? CambioFaseConsistente { get; set; }
            public string? ComentarioConsistenciaCambioFase { get; set; }
            public bool Estado { get; set; }
            public string UsuarioCreacion { get; set; }
            public string UsuarioModificacion { get; set; }
            public DateTime FechaCreacion { get; set; }
            public DateTime FechaModificacion { get; set; }

            public int? FraseCombinadaId { get; set; }
            public int? FC_Channel { get; set; }
            public string FC_Lexical { get; set; }
            public string FC_ITN { get; set; }
            public string FC_MaskedITN { get; set; }
            public string FC_Display { get; set; }

            public int? FraseReconocidaId { get; set; }
            public string RecognitionStatus { get; set; }
            public int? FR_Channel { get; set; }
            public string Speaker { get; set; }
            public string Offset { get; set; }
            public string FR_Duration { get; set; }
            public long? OffsetInTicks { get; set; }
            public long? DurationInTicksFraseReconocida { get; set; }
            public int? DurationMillisecondsFraseReconocida { get; set; }
            public int? OffsetMilliseconds { get; set; }

            public int? DetalleFraseReconocidaId { get; set; }
            public decimal? Confidence { get; set; }
            public string DFR_Lexical { get; set; }
            public string DFR_ITN { get; set; }
            public string DFR_MaskedITN { get; set; }
            public string DFR_Display { get; set; }
            public string DFR_Sentiment { get; set; }

            public int? RecomendacionId { get; set; }
            public string Recomendacion { get; set; }
            public bool? RT_Estado { get; set; }
            public string RT_UsuarioCreacion { get; set; }
            public string RT_UsuarioModificacion { get; set; }
            public DateTime? RT_FechaCreacion { get; set; }
            public DateTime? RT_FechaModificacion { get; set; }
        }


        
    }
}
