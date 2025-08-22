using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TipoContribuyente : BaseIntegraEntity
    {
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
    }
}
