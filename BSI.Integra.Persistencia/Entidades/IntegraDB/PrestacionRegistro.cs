using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PrestacionRegistro : BaseIntegraEntity
    {
        public int IdPrestacionTipo { get; set; }
        [StringLength(500)]
        public string Nombre { get; set; } = null!;



    }
}
