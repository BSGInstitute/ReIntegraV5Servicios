using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PreguntaFrecuente : BaseIntegraEntity
    {
        public int IdSeccionPreguntaFrecuente { get; set; }
        public string Pregunta { get; set; } = null!;
        public string Respuesta { get; set; } = null!;
        public int? Tipo { get; set; }

        public List<PreguntaFrecuentePGeneral> PreguntaFrecuentePgeneral { get; set; }
        public List<PreguntaFrecuenteArea> PreguntaFrecuenteArea { get; set; }
        public List<PreguntaFrecuenteSubArea> PreguntaFrecuenteSubArea { get; set; }
        public List<PreguntaFrecuenteTipo> PreguntaFrecuenteTipo { get; set; }
       





    }
}
