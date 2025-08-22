using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProveedorSubCriterioCalificacion : BaseIntegraEntity
    {

        public int IdProveedorCriterioCalificacion { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        public int Puntaje { get; set; }
    }
}
