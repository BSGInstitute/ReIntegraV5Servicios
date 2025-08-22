using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SemaforoFinancieroDetalle : BaseIntegraEntity
    {
        public int IdSemaforoFinanciero { get; set; }
        [StringLength(200)]
        public string Nombre { get; set; } = null!;
        [StringLength(800)]
        public string Mensaje { get; set; } = null!;
        [StringLength(10)]
        public string Color { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }
}
