
using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class TagPw : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int? TagWebId { get; set; }
        public string? Codigo { get; set; }
        public virtual ICollection<PgeneralTagsPw> PgeneralTagsPws { get; set; }
        public virtual ICollection<TagParametroSeoPw> TagParametroSeoPws { get; set; }
    }
}
