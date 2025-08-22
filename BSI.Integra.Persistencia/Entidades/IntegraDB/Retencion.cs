using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Retencion : BaseIntegraEntity
    {
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [StringLength(70)]
        public string? Descripcion { get; set; }
        public decimal Valor { get; set; }
        public int? IdPais { get; set; }
    }
}
