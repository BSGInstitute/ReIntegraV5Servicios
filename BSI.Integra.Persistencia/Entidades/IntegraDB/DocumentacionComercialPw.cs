using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DocumentacionComercialPw : BaseIntegraEntity
    {
        [StringLength(50)]
        public string Titulo { get; set; } = null!;
        public string Contenido { get; set; } = null!;
        [StringLength(50)]
        public string Tipo { get; set; } = null!;
        [StringLength(50)]
        public string Modalidad { get; set; } = null!;
        public int? IdPais { get; set; }
    }
}
