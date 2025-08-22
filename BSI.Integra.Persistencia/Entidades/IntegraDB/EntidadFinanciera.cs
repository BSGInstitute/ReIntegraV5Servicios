using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class EntidadFinanciera : BaseIntegraEntity
    {
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        [StringLength(100)]
        public string Descripcion { get; set; } = null!;

        public int IdMoneda { get; set; }
        [StringLength(50)]
        public string CuentaCte { get; set; } = null!;
        public int IdProveedor { get; set; }

    }
}
