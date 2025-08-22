using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TipoContrato : BaseIntegraEntity
    {
        [StringLength(150)]
        public string Nombre { get; set; } = null!;
        [StringLength(250)]
        public string Comentario { get; set; } = null!;
        public int IdPais { get; set; }
    }
}
