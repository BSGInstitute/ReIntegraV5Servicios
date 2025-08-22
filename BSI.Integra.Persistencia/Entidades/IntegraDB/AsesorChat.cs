using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class AsesorChat : BaseIntegraEntity
    {
        public int? IdPersonal { get; set; }
        public string NombreAsesor { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
