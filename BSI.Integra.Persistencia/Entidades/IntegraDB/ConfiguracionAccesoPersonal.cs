using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ConfiguracionAccesoPersonal : BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public int IdPersonalAcceso { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public int IdModuloSistema { get; set; }
    }
}
