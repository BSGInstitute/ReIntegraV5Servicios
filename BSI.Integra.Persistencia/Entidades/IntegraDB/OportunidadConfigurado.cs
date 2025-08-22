using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class OportunidadConfigurado : BaseIntegraEntity
    {
        [StringLength(100)]
        public string Nombre { get; set; }

    }
}
