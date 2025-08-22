using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CuentaContablePadre : BaseIntegraEntity
    {
        public int CuentaPadre { get; set; }
        [StringLength(500)]
        public string Descripcion { get; set; } = null!;
    }
}
