using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PreguntaFrecuenteTipo : BaseIntegraEntity
    {
        public int? IdPreguntaFrecuente { get; set; }
        public int IdTipo { get; set; }
        
    }
}
