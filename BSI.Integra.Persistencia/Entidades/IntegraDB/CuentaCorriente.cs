using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CuentaCorriente : BaseIntegraEntity
    {
        [StringLength(50)]
        public string NumeroCuenta { get; set; } = null!;
        public int? IdCiudad { get; set; }
        [StringLength(10)]
        public string? Sucursal { get; set; }
        public int? IdMoneda { get; set; }
        [StringLength(10)]
        public string? Cuenta { get; set; }
        public int? IdBanco { get; set; }
    }
}
