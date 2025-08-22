using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TipoComprobante : BaseIntegraEntity
    {
        public int IdPais { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
