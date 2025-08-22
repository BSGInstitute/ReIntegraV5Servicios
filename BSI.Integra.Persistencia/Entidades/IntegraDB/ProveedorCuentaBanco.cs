using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProveedorCuentaBanco : BaseIntegraEntity
    {
        public int IdProveedor { get; set; }
        public int IdEntidadFinanciera { get; set; }
        public int IdTipoCuentaBanco { get; set; }
        [StringLength(50)]
        public string NroCuenta { get; set; } = null!;
        [StringLength(50)]
        public string CuentaInterbancaria { get; set; } = null!;
        public int IdMoneda { get; set; }
    }
}
