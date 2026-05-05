using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial
{
    public class ColorPerfilProgramaV2DTO
    {
        [JsonProperty("programa")]
        public string Programa { get; set; }
        [JsonProperty("probabilidad_inscripcion")]
        public string ProbabilidadInscripcion { get; set; }
    }
}
