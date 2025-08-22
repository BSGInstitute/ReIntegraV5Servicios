using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ClasificacionPersona : BaseIntegraEntity
    {
        public int? IdPersona { get; set; }
        public int IdTipoPersona { get; set; }
        public int IdTablaOriginal { get; set; }
        public int? IdMigracion { get; set; }
    }
}
