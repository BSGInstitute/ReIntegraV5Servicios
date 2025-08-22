using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Caja : BaseIntegraEntity
    {
        [StringLength(50)]
        public string CodigoCaja { get; set; } = null!;
        public int IdMoneda { get; set; }
        public int IdEmpresaAutorizada { get; set; }
        public int IdEntidadFinanciera { get; set; }
        public int IdCuentaCorriente { get; set; }
        public int IdCiudad { get; set; }
        public int IdPersonalResponsable { get; set; }
        public bool Activo { get; set; }
    }
}
