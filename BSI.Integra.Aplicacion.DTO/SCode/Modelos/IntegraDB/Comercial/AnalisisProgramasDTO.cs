using Newtonsoft.Json;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial
{
    public class AnalisisProgramasAlumnoDTO
    {
        [JsonProperty("ultima_oportunidad")]
        public ProgramaProbabilidadDTO UltimaOportunidad { get; set; }

        [JsonProperty("mayor_probabilidad")]
        public List<ProgramaProbabilidadDTO> MayorProbabilidad { get; set; } = new List<ProgramaProbabilidadDTO>();

        [JsonProperty("venta_cruzada")]
        public List<ProgramaProbabilidadDTO> VentaCruzada { get; set; } = new List<ProgramaProbabilidadDTO>();
    }

    public class ProgramaProbabilidadDTO
    {
        [JsonProperty("idPGeneral")]
        public int? IdPGeneral { get; set; }

        [JsonProperty("programa")]
        public string Programa { get; set; }

        [JsonProperty("subarea")]
        public string SubArea { get; set; }

        [JsonProperty("area")]
        public string Area { get; set; }

        [JsonProperty("probabilidad")]
        public string Probabilidad { get; set; }
    }
}
