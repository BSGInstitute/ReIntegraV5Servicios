using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PgeneralParametroSeoPw : BaseIntegraEntity
    {
        [StringLength(8000)]
        public string Descripcion { get; set; } = null!; 
        public int? IdPgeneral { get; set; } 
        public int IdParametroSeo { get; set; }
    }
}
