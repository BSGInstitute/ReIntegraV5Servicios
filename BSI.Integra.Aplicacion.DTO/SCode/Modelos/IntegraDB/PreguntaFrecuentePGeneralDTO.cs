using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PreguntaFrecuentePGeneralDTO
    {
        public int Id { get; set; }
        public int? IdPreguntaFrecuente { get; set; }
        public int? IdPgeneral { get; set; }
    }
    public class PreguntaFrecuentePorCentroCostoDTO
    {
        public int? IdPreguntaFrecuente { get; set; }
        public int? IdPrograma { get; set; }
        public int? IdSeccion { get; set; }
        public string? Nombre { get; set; }
        public string? Pregunta { get; set; }
        public string? Respuesta { get; set; }
    }
    public class PreguntaFrecuenteDetallePorCentroCostoDTO
    {
        public int? IdPrograma { get; set; }
        public int? IdSeccion { get; set; }
        public string? Nombre { get; set; }
        public List<PreguntaFrecuentePreguntaRespuestaDTO> Detalle { get; set; } = new List<PreguntaFrecuentePreguntaRespuestaDTO>();
    }
    public class PreguntaFrecuentePGeneralRespuestaDTO
    {
        public int? Id { get; set; }
        public int? IdPrograma { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public int? IdTipo { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public int? IdSeccion { get; set; }
        public string Nombre { get; set; }
    }
}
