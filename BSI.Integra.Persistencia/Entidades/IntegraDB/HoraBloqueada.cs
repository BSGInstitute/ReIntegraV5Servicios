using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class HoraBloqueada : BaseIntegraEntity
    {
        public int? IdPersonal { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
    }
}
