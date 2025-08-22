using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Cargo : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public int? Orden { get; set; }

    }
}
