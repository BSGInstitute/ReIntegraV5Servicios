using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PreguntaFrecuenteSubArea : BaseIntegraEntity
    {
        public int? IdPreguntaFrecuente { get; set; }
      
        public int IdSubArea { get; set; }
       
    }
}
