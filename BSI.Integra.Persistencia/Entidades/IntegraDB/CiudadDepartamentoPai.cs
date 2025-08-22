using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CiudadDepartamentoPai : BaseIntegraEntity
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Codigo { get; set; }
        [StringLength(100)]
        public string Nombre { get; set; }
        public int IdDepartamentoPais { get; set; }
    }
}
