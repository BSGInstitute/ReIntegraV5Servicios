using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ContadorBicLogDetalle : BaseIntegraEntity
    {
        public int IdContadorBicLog { get; set; }
        public bool EstadoContactoManhana { get; set; }
        public bool EstadoContactoTarde { get; set; }
        public DateTime FechaLogContacto { get; set; }
    }
}
