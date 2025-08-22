using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class AreaCapacitacion : BaseIntegraEntity
    {
        [StringLength(150)]
        public string Nombre { get; set; } = null!;
        [StringLength(100)]
        public string? Descripcion { get; set; }
        [StringLength(150)]
        public string? ImgPortada { get; set; }
        [StringLength(150)]
        public string? ImgSecundaria { get; set; }
        [StringLength(150)]
        public string? ImgPortadaAlt { get; set; }
        [StringLength(150)]
        public string? ImgSecundariaAlt { get; set; }
        public bool EsVisibleWeb { get; set; }
        public int? IdArea { get; set; }
        public bool EsWeb { get; set; }
        [StringLength(3000)]
        public string? DescripcionHtml { get; set; }
        public int? IdAreaCapacitacionFacebook { get; set; }
        public List<AreaParametroSeoPw> AreaParametroSeoPw { get; set; }
    }
}
