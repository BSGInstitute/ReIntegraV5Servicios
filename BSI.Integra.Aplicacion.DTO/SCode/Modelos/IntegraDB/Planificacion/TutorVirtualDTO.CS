using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class TutorVirtualDTO
    {
    }
    public class ProcesamientoTranscripcionVideoOnlineDTO
    {
        public int IdProcesamientoTranscripcionVideoAOnline { get; set; }
        public string VideoId { get; set; }
        public int Plataforma { get; set; }
        public int NroErrores { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public DateTime FechaProcesamiento { get; set; }
    }
    public class ProcesarVideoProcesamientoAonlineDTO
    {
        public string video_id { get; set; }

        public int plataforma { get; set; }
    }
    public class ProcesamientoVideosAonlineRequestDTO
    {
        public List<ProcesarVideoProcesamientoAonlineDTO> videos { get; set; } = new List<ProcesarVideoProcesamientoAonlineDTO>();
        public string? usuario { get; set; }
    }
    public class ProcesamientoVideosAonlineEnvioDTO
    {
        public List<ProcesarVideoProcesamientoAonlineDTO> Videos { get; set; } = new List<ProcesarVideoProcesamientoAonlineDTO>();
        public string? Usuario { get; set; }
    }
    public class ProcesamientoVideosAonlineEnvioCorreoDTO
    {
        public List<VideoInfoDTO> Videos { get; set; }
        public string Usuario { get; set; }
    }

    public class VideoInfoDTO
    {
        [JsonPropertyName("video_id")]
        public string VideoId { get; set; }

        public int Plataforma { get; set; }

        public bool Completado { get; set; }
    }
}
