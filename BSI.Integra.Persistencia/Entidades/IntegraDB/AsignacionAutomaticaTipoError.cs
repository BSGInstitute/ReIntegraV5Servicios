using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class AsignacionAutomaticaTipoError : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;

        public Guid? IdMigracion { get; set; }
    }

}
