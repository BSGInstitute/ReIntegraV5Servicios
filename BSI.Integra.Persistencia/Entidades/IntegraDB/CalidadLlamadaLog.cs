using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CalidadLlamadaLog : BaseIntegraEntity
    {
        public int IdProblemaLlamada { get; set; }
        public int? IdCalidadLlamada { get; set; }
        public int? IdActividadDetalle { get; set; }
    }
}
