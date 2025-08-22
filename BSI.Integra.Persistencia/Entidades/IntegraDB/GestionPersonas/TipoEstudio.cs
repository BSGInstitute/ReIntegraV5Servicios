using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class TipoEstudio : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }
}
