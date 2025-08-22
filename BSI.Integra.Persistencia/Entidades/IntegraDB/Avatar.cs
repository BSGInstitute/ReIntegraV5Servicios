using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Avatar : BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        [StringLength(50)]
        public string Top { get; set; } = null!;
        [StringLength(50)]
        public string Accessories { get; set; } = null!;
        [StringLength(50)]
        public string HairColor { get; set; } = null!;
        [StringLength(50)]
        public string FacialHair { get; set; } = null!;
        [StringLength(50)]
        public string FacialHairColor { get; set; } = null!;
        [StringLength(50)]
        public string Clothes { get; set; } = null!;
        [StringLength(50)]
        public string Eyes { get; set; } = null!;
        [StringLength(50)]
        public string Eyesbrow { get; set; } = null!;
        [StringLength(50)]
        public string Mouth { get; set; } = null!;
        [StringLength(50)]
        public string Skin { get; set; } = null!;
        [StringLength(50)]
        public string ClothesColor { get; set; } = null!;
    }
}
