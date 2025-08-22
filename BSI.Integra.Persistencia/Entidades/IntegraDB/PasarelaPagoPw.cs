using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PasarelaPagoPw : BaseIntegraEntity
    {
        [StringLength(150)]
        public string Nombre { get; set; } = null!;
        public int? IdProveedor { get; set; }
        public int? IdPais { get; set; }
        public int? Prioridad { get; set; }
    }
}
