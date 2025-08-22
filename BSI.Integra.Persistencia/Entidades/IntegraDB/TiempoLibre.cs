using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TiempoLibre : BaseIntegraEntity
    {
        public int TiempoMin { get; set; }
        public int Tipo { get; set; }
    }
}
