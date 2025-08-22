using Google.Api.Ads.AdWords.v201809;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteAdwordsApiPalabrasClaveRespuestaDTO
    {
        public string Pais { get; set; }
        public int IdPais { get; set; }
        public List<PalabraClaveVolumenDTO> Detalle { get; set; }

    }

    public class PalabraClaveVolumenDTO
    {
        public string PalabraClave { get; set; }
        public MonthlySearchVolume[] PromedioPorMes { get; set; }
    }

}
