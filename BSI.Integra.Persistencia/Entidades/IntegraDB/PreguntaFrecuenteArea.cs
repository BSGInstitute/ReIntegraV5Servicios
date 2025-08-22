using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PreguntaFrecuenteArea : BaseIntegraEntity
    {
        public int? IdPreguntaFrecuente { get; set; }
        public int IdArea { get; set; }
      
    }
}
