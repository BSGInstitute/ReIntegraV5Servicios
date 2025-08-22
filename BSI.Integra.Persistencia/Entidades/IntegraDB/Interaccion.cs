using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Interaccion : BaseIntegraEntity
    {
        public int? IdActividadDetalle { get; set; }
        public int? IdTipoInteraccionGeneral { get; set; }
        public DateTime Fecha { get; set; }
    }
}
