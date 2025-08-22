using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SemaforoFinancieroVariable : BaseIntegraEntity
    {
        [StringLength(500)]
        public string Nombre { get; set; } = null!;
        public bool? AplicaUnidad { get; set; }
    }
}
