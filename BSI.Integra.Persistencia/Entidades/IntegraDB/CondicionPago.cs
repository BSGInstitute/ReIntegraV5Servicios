using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CondicionPago : BaseIntegraEntity
    {
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

    }
}
