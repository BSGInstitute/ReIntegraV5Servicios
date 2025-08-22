using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SubAreaCapacitacion : BaseIntegraEntity
    {
        [StringLength(250)]
        public string Nombre { get; set; } = null!;
        [StringLength(550)]
        public string? Descripcion { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public bool EsVisibleWeb { get; set; }
        public int? IdSubArea { get; set; }
        [StringLength(3000)]
        public string? DescripcionHtml { get; set; }
        [StringLength(20)]
        public string AliasFacebook { get; set; } = null!;
        public   ICollection<SubAreaParametroSeoPw> SubAreaParametroSeoPws { get; set; } 
    }
}
