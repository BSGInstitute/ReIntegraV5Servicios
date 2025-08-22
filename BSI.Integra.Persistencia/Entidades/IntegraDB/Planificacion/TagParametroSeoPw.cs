using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class TagParametroSeoPw : BaseIntegraEntity
    {
        public string Descripcion { get; set; } = null!;
        public int IdTagPw { get; set; }
        public int IdParametroSeopw { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
